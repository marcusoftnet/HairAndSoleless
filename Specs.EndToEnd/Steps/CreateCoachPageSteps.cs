using Should.Fluent;
using Specs.EndToEnd.Steps.Infrastructure;
using Specs.EndToEnd.Steps.PageObjects;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class CreateCoachPageSteps : StepsForPage<CreateCoachPage>
    {
      

        [Given(@"I am on the 'Create Coach' page")]
        public void BeOnTheCreateCoachPage()
        {
            Page.Visit();
        }

        [When(@"I create the following coach")]
        public void WhenICreateTheFollowingCoach(Table table)
        {
            // TODO: Use FillInstance when SpecFlow 1.6 is installed
            Page.NameField = table.Rows[0]["Name"];
            Page.EmailField = table.Rows[0]["Email"];
            Page.TeamField = table.Rows[0]["Team"];
            Page.Submit();
        }

        [When(@"I create a coach with '(.*)' set to empty")]
        public void CreateCoachWithFieldEmpty(string fieldName)
        {
            Page.SetDefaultData();
            Page.SetValue(fieldName, string.Empty);
            Page.Submit();
        }
    }
}
