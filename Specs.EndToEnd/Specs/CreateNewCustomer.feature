Feature: Create customer
	In order to keep track of the different customer we have been at
	As an Avega Coach
	I want to be able to create a new customer

Background: 
	Given there are no customers named 'Testing Inc.' in the database
	
Scenario: Navigate to the new customer page
	Given I am on the 'Customers' page
	When I click the 'Create New' link
	Then I should be on the 'Create Customer' page

Scenario: Creating new customer without validation errors
	Given I am on the 'Create Customer' page
	When I create the following customer
		| Name         | Contact           | 
		| Testing Inc. | demon@testinc.com |
	Then I should be on the 'Customers' page
		And a customer named 'Testing Inc.' should be in the customer list

Scenario: Creating new customer with no name should give validation error
	Given I am on the 'Create Customer' page
	When I create the following customer
		| Name         | Contact           | 
		|              | demon@testinc.com |
	Then a validation error for 'Name' should be displayed
		And I should still be on the 'Create Customer' page

Scenario: Creating new customer with no email should give validation error
	Given I am on the 'Create Customer' page
	When I create the following customer
		| Name         | Contact | 
		| Testing Inc  |         |
	Then a validation error for 'Contact' should be displayed
		And I should still be on the 'Create Customer' page
		
	   	