Feature: DnsErrors
	Check errors returned by DNS server

Scenario: Check error is returned when more than one question is passed
	When execute DNS request
	| Label			| Class | Type |
	| example.com	| IN	| STAR |
	| example.com	| IN	| STAR |

	Then DNS flag is equal to 'RESPONSE|REFUSED'

Scenario: Check error is returned when requested type is not supported
	When execute DNS request
	| Label			| Class | Type			|
	| example.com	| IN    | NOT_SUPPORTED |
	
	Then DNS flag is equal to 'RESPONSE|NOT_IMPLEMENTED'

Scenario: Check error is returned when requested class is not supported
	When execute DNS request
	| Label			| Class				| Type			|
	| example.com	| NOT_SUPPORTED		| STAR			|
	
	Then DNS flag is equal to 'RESPONSE|FORMAT_ERROR'