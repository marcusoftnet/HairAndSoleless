using Specs.EndToEnd.Steps.Infrastructure;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class DBSteps
    {
        [Given(@"there are no customers named '(.*)' in the database")]
        public void RemoveCustomers(string customerName)
        {
            DbHelper.RemoveCustomersByName(customerName);
        }

        [Given(@"there are no coaches named '(.*)' in the database")]
        public void RemoveCoaches(string coachName)
        {
            DbHelper.RemoveCoachesByName(coachName);
        }

    }
}
