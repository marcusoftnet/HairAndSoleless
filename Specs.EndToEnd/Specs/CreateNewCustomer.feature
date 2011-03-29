Feature: Create customer
	In order to keep track of the different customer we have been at
	As an Avega Coach
	I want to be able to create a new customer
		
Scenario: Navigate to the new customer page
	Given I am on the 'Customers' page
	When I choose to create new customer
	Then I should be on the 'Create Customer' page

Scenario: Creating new customer without validation errors
	Given there are no customers named 'Testing Inc.' in the database
		And I am on the 'Create Customer' page
	When I create the following customer
		| Name         | Contact           | 
		| Testing Inc. | demon@testinc.com |
	Then I should be on the 'Customers' page
		And a customer named 'Testing Inc.' should be in the customer list

Scenario Outline: Creating new customer with missing values should give validation error
    Given I am on the 'Create Customer' page
	When I create a customer with '<Field>' set to empty
	Then a required field validation error for '<Field>' should be displayed
		And I should still be on the 'Create Customer' page
		
Examples:
	| Field   |
	| Name    |
	| Contact |

	   	