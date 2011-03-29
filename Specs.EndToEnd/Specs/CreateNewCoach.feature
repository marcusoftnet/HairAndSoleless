Feature: Create coach
	In order to keep track of the different coaches that submit activities
	As an Avega Coach
	I want to be able to create a new coach

Scenario: Navigate to the new coach page
	Given I am on the 'Coahces' page
	When I choose to create new coach
	Then I should be on the 'Create Coach' page

Scenario: Creating new coach without validation errors
	Given there are no coaches named 'Test Coach' in the database
		And I am on the 'Create Coach' page
	When I create the following coach
		| Name       | Email         | Team      | 
		| Test Coach | test@avega.se | Test team |
	Then I should be on the 'Coaches' page
		And a customer named 'Test Coach' should be in the coach list

Scenario Outline: Creating new coach with missing values should give validation error
	Given I am on the 'Create Coach' page
	When I create a coach with '<Field>' set to empty
	Then a validation error for '<Field>' should be displayed
		And I should still be on the 'Create Coach' page

Examples:
	| Field |
	| Name  |
	| Email |
	| Team  |


