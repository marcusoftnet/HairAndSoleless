﻿using Specs.EndToEnd.Steps.PageObjects;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class CustomerCreateSteps : StepsForPage<CustomersCreatePage>
    {
        [When(@"I create the following customer")]
        public void ICreateTheFollowingCustomer(Table table)
        {
            // TODO: Use FillInstance when SpecFlow 1.6 is installed
            Page.NameField = table.Rows[0]["Name"];
            Page.ContactField = table.Rows[0]["Contact"];
            Page.Submit();
        }

        [Given(@"I am on the 'Create Customer' page")]
        public void IAmOnTheCreateCustomerPage()
        {
            Page.Visit();
        }
        
        [When(@"I create a customer with '(.*)' set to empty")]
        public void CreateCustomerWithFieldEmpty(string fieldName)
        {
            Page.SetDefaultData();
            Page.SetValue(fieldName, string.Empty);
            Page.Submit();
        }

    }
}
