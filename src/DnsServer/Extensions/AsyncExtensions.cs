// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer.Extensions
{
    public static class AsyncExtensions
    {
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken, int? timeOutMilliSeconds = null)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                var tasks = new List<Task>
                {
                    task, tcs.Task
                };

                Task delayTask = null;
                if (timeOutMilliSeconds != null)
                {
                    delayTask = Task.Delay(timeOutMilliSeconds.Value);
                    tasks.Add(delayTask);
                }

                if (task != await Task.WhenAny(tasks))
                {
                    if (delayTask.IsCompleted)
                    {
                        throw new TimeoutException();
                    }

                    throw new OperationCanceledException(cancellationToken);
                }
            }

            return task.Result;
        }
    }
}
