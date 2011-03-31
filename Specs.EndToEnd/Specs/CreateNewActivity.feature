Feature: Create new activity
	In order to keep track of the different acitivites that coaches has made
	As an Avega Coach
	I want to be able to create a new activity

Scenario: Navigate to the new activity page
	Given I am on the 'Activities' page
	When I choose to create new activity
	Then I should be on the 'Create Activity' page

Scenario: Creating new activity without validation errors
	Given there are no activites for coach 'Test Coach' in the database
		And there is a coach named 'Test Coach' in the database
		And there is a customer named 'Test Customer' in the database
		And I am on the 'Create Activity' page
	When I create the following activity
		| Heading      | NumberOfHours | Date       | Coach      | Customer      |
		| BDD Workshop | 12            | 2011/01/01 | Test Coach | Test Customer |
	Then I should be on the 'Activities' page
		And an activity with the heading 'BDD Workshop' should be in the activities list

Scenario Outline: Creating new activity with missing values should give validation error
	Given I am on the 'Create Activity' page
	When I create an activity with '<Field>' set to empty
	Then a required field validation error for '<Error message>' should be displayed
		And I should still be on the 'Create Activity' page

Examples:
	Examples:
	| Field         | Error message   |
	| Heading       | Heading         |
	| CoachId       | Coach           |
	| CustomerId    | Customer        |
	| Date          | Date            |
	| NumberOfHours | Number of hours |