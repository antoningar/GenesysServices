Feature: Overdrawn
Check if a client is account is overdrawn
    
Scenario: Is client overdrawn
    Given a client <clientId>
    And his account balance is <balance>
    And my system already know this client
    When I check if this cliet is overdrawm
    Then I got response <isOverdrawn>
    
Examples:
| clientId | balance | isOverdrawm |
| 1        | 100     | false       |
| 2        | 0       | false       |
| 3        | -100    | true        |
| 4        | -.1     | true        |