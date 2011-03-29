using Should.Fluent;
using Specs.EndToEnd.Steps.PageObjects;
using TechTalk.SpecFlow;
namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class CoachesSteps : StepsForPage<CoachesPage>
    {
        [Given(@"I am on the 'Coahces' page")]
        public void GivenIAmOnTheCoahcesPage()
        {
            Page.Visit();
        }

        [When(@"I choose to create new coach")]
        public void ChooseCreateCustomer()
        {
            Page.GotoNewCoach();
        }

        [Then(@"I should be on the 'Coaches' page")]
        public void ThenIShouldBeOnTheCoachesPage()
        {
            Page.Title.Should().Equal("Coaches"); // TODO: General step?
        }

        [Then(@"a customer named '(.*)' should be in the coach list")]
        public void ThenACustomerNamedTestCoachShouldBeInTheCoachList(string coachName)
        {
            Page.CoachNames.Should().Contain.One(coachName);
        }
    }
}
