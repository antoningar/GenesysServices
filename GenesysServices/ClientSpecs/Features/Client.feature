Feature: Get client number
Get client number from a phone number
    
    Scenario: Get client number
        Given a client with phone number <callerNumber>
        And a database client with an unique client with phone number <clientNumber>
        And client id <clientId>
        When I call client service
        Then I receive this <clientIdResponse> as a response
        
    Examples:
    | callerNumber | clientNumber | clientId | clientIdResponse |
    | 0600000000 | 0600000000 | 123 | 123 |
    | 0600000001 | 0600000000 | 123 |  |