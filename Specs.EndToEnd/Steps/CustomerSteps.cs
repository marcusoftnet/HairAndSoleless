using Should.Fluent;
using Specs.EndToEnd.Steps.PageObjects;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class CustomersSteps : StepsForPage<CustomersPage>
    {
        [Given(@"I am on the 'Customers' page")]
        public void IAmOnTheCustomerPage()
        {
            Page.Visit();
        }

        [When(@"I choose to create new customer")]
        public void ClickCreateNewLink()
        {
            Page.GotoNewCustomer();
        }
        
        [Then(@"a customer named '(.*)' should be in the customer list")]
        public void CustomerNameAmongNames(string customerName)
        {
            Page.CustomerNames.Should().Contain.One(customerName);
        }

    }
}
