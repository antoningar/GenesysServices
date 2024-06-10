Feature: ISBN
Check if input is a valid ISBN
    
    Scenario: Check ISBN
        Given an ISBN <isbn>
        And ISBN soap service return <valid>
        When I check if my ISBN is valid
        Then I got response <valid>
        
    Examples:
    | isbn          | valid |
    | 9798507236343 | true  |
    | 9798507236342 | false |
    | 979850723634  | false | 