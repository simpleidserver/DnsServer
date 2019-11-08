Feature: Dns
	Check DNS server

Scenario: Check CNAME are returned by the autoritative DNS server
	When execute DNS request
	| Label			| Class | Type	|
	| example.com	| IN	| CNAME |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.CNAME' equals to 'www.example.net'
	Then key 'Answers[1].ResourceRecord.CNAME' equals to 'www.example.org'
	Then key 'Answers[2].ResourceRecord.CNAME' equals to 'www.example.com'
	
Scenario: Check HINFO is returned by authoritative DNS server
	When execute DNS request
	| Label			| Class | Type	|
	| example.com	| IN	| HINFO |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.CPU' equals to 'intel'
	Then key 'Answers[0].ResourceRecord.OS' equals to 'win'

Scenario: Check MB is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type |
	| example.com | IN    | MB   |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.MADNAME' equals to 'mail.com'

Scenario: Check MG is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type |
	| example.com | IN    | MG   |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.MGMNAME' equals to 'group.mail.com'

Scenario: Check MINFO is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type  |
	| example.com | IN    | MINFO |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.EMAILBX' equals to 'admin@mail.com'
	Then key 'Answers[0].ResourceRecord.RMAILBX' equals to 'error@mail.com'

Scenario: Check MR is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type |
	| example.com | IN    | MR   |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.NEWNAME' equals to 'name@mail.com'

Scenario: Check MX is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type |
	| example.com | IN    | MX   |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.Preference' equals to '4'
	Then key 'Answers[0].ResourceRecord.Exchange' equals to 'mail.com'

Scenario: Check NS is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type |
	| example.com | IN    | NS   |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.NSDName' equals to 'example.com'

Scenario: Check PTR is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type |
	| example.com | IN    | PTR  |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.PTRDNAME' equals to 'example.com'

Scenario: Check SOA is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type |
	| example.com | IN    | SOA  |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.MName' equals to 'example.com'
	Then key 'Answers[0].ResourceRecord.RName' equals to 'mail.example.com'
	Then key 'Answers[0].ResourceRecord.Serial' equals to '9999'
	Then key 'Answers[0].ResourceRecord.Refresh' equals to '1000'
	Then key 'Answers[0].ResourceRecord.Retry' equals to '1001'
	Then key 'Answers[0].ResourceRecord.Expire' equals to '1002'
	Then key 'Answers[0].ResourceRecord.Minimum' equals to '10'

Scenario: Check TXT is returned by the authoritative DNS server
	When execute DNS request
	| Label       | Class | Type |
	| example.com | IN    | TXT  |

	Then DNS flag is equal to 'RESPONSE'
	Then key 'Answers[0].ResourceRecord.TxtData' equals to 'test'

Scenario: Check A is returned by the recursive DNS server
	When execute DNS request
	| Label      | Class | Type |
	| google.com | IN    | A    |

	And execute DNS request
	| Label      | Class | Type |
	| google.com | IN    | A    |

	Then DNS flag is equal to 'RESPONSE'
