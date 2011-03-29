using Specs.EndToEnd.Steps.Infrastructure;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class DBSteps
    {
        [Given(@"there are no customers named '(.*)' in the database")]
        public void ThereAreNoCustomersInDbNamed(string customerName)
        {
            DbHelper.RemoveAllCustomersNamed(customerName);
        }
    }
}
