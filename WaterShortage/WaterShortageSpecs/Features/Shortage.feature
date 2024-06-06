Feature: Shortage
Get Shortage by SIREN

    Scenario: SIREN error
        Given SIREN api is down
        When I would like to know if my companies is vulnerable to water shortage in my region
        Then I got a response <responseCode>
    
    Examples:
      | responseCode |
      | 500          |

    Scenario: Water error
        Given Water api is down
        When I would like to know if my companies is vulnerable to water shortage in my region
        Then I got a response <responseCode>
    
    Examples:
      | responseCode |
      | 500          |

    Scenario: There is 
        Given My company operate in <postalCode>
        And There is a a <gravity> shortage
        When I would like to know if my companies is vulnerable to water shortage in my region
        Then I got a response <responseCode>
    
    Examples:
      | postalCode | gravity          | responseCode |
      | 64000      | alerte_renforcee | 200          |
