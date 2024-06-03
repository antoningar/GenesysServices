Feature: Shortage
Get Shortage by SIREN

    Scenario: SIREN error
        Given SIREN api is down
        When I would like to know if my companies is vulnerable to water shortage in my region
        Then I got a respones <responseCode>
        And reason is <reason>
    
    Examples:
      | responseCode | reason                      |
      | 500          | External SIREN service down |

    Scenario: Water error
        Given Water api is down
        When I would like to know if my companies is vulnerable to water shortage in my region
        Then I got a response <responseCode>
        And reason is <reason>
    
    Examples:
      | responseCode | reason                      |
      | 500          | External water service down |

    Scenario: There is 
        Given My company operate in <region>
        And There is a a <gravity> shortage
        When I would like to know if my companies is vulnerable to water shortage in my region
        Then I got a response <errorCode>
    
    Examples:
      | responseCode |
      | 200          |
