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

        [Given(@"there are no activites for coach '(.*)' in the database")]
        public void NoActivitiesForCoachWithName(string coachToDeleteActivitesFor)
        {
            DbHelper.RemoveActivitiesForCoachByName(coachToDeleteActivitesFor);
        }

        [Given(@"there is a coach named '(.*)' in the database")]
        public void CreateCoachByName(string coachName)
        {
            DbHelper.CreateCoach(coachName, "test@email.com", "Test team");
        }

        [Given(@"there is a customer named '(.*)' in the database")]
        public void CreateCustomerByName(string customerName)
        {
            DbHelper.CreateCustomer(customerName, "test@test.com");
        }



    }
}
