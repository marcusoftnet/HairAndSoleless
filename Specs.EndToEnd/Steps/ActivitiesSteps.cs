using Should.Fluent;
using Specs.EndToEnd.Steps.PageObjects;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class ActivitiesSteps : StepsForPage<ActivitiesPage>
    {
        [Given(@"I am on the 'Activities' page")]
        public void GivenIAmOnTheActivitiesPage()
        {
            Page.Visit();
        }

        [When(@"I choose to create new activity")]
        public void WhenIChooseToCreateNewActivity()
        {
            Page.GotoNewActivity();
        }

        [Then(@"an activity with the heading '(.*)' should be in the activities list")]
        public void ActivityInList(string activityHeading)
        {
           Page.ActivitiyHeadings.Should().Contain.One(activityHeading);
        }

    }
}
