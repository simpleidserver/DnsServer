// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Messages;
using DnsServer.Messages.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace DnsServer.Acceptance.Tests.Steps
{
    [Binding]
    public class SharedStep
    {
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private static object lck = new object();
        private ScenarioContext _scenarioContext;

        public SharedStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            lock(lck)
            {
                DnsStartup.GetInstance().Start();
            }
        }

        [When("execute DNS request")]
        public async Task WhenExecuteDnsQuery(Table table)
        {
            var builder = new DNSRequestMessageBuilder()
                .New();
            foreach(var record in table.Rows)
            {
                var label = record["Label"];
                var qClass = GetPropValue<FakeQuestionClasses>(record["Class"]);
                var qType = GetPropValue<FakeQuestionTypes>(record["Type"]);
                builder.AddQuestion(label, qClass, qType);
            }

            var dnsResponseMessage = await SendRequest(builder.Build());
            var jObj = JObject.Parse(JsonConvert.SerializeObject(dnsResponseMessage));
            _scenarioContext.Remove("dnsResponse");
            _scenarioContext.Remove("dnsResponseJSON");
            _scenarioContext.Add("dnsResponse", dnsResponseMessage);
            _scenarioContext.Add("dnsResponseJSON", jObj);
        }

        [Then("DNS flag is equal to '(.*)'")]
        public void ThenDNSFlagEqualsTo(string str)
        {
            DNSHeaderFlags dnsHeaderFlag = null;
            foreach (var s in str.Split('|'))
            {
                var hf = GetPropValue<DNSHeaderFlags>(s);
                if (dnsHeaderFlag == null)
                {
                    dnsHeaderFlag = hf;
                }
                else
                {
                    dnsHeaderFlag.SetFlag(hf);
                }
            }
            
            var dnsResponseMessage = _scenarioContext.Get<DNSResponseMessage>("dnsResponse");
            Assert.Equal(dnsHeaderFlag.Value, dnsResponseMessage.Header.Flag.Value);
        }

        [Then("key '(.*)' equals to '(.*)'")]
        public void ThenEqualsTo(string key, string value)
        {
            var jObj = _scenarioContext.Get<JObject>("dnsResponseJSON");
            var token = jObj.SelectToken(key);
            Assert.Equal(token.ToString().ToLowerInvariant(), value.ToLowerInvariant());
        }

        private async Task<DNSResponseMessage> SendRequest(DNSRequestMessage request)
        {
            await _semaphoreSlim.WaitAsync();
            var udpClient = new UdpClient();
            var payload = request.Serialize().ToArray();
            await udpClient.SendAsync(payload, payload.Count(), new IPEndPoint(IPAddress.Parse("127.0.0.1"), 53));
            var resultPayload = await udpClient.ReceiveAsync();
            _semaphoreSlim.Release();
            return DNSResponseMessage.Extract(resultPayload.Buffer);
        }

        private static T GetPropValue<T>(string propName)
        {
            var propertyInfo = typeof(T).GetMember(propName, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
            var instance = ((FieldInfo)propertyInfo[0]).GetValue(null);
            var valProperty = instance.GetType().GetProperty("Value");
            var val = (UInt16)valProperty.GetValue(instance);
            return (T)Activator.CreateInstance(typeof(T), val);
        }
    }

    public class FakeQuestionClasses : QuestionClasses
    {
        public FakeQuestionClasses(UInt16 value) : base(value)
        {
        }

        public static FakeQuestionClasses NOT_SUPPORTED = new FakeQuestionClasses(999);
    }

    public class FakeQuestionTypes : QuestionTypes
    {
        public FakeQuestionTypes(UInt16 value) : base(value)
        {
        }

        public static FakeQuestionClasses NOT_SUPPORTED = new FakeQuestionClasses(999);
    }
}
