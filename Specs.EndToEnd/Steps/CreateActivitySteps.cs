using Specs.EndToEnd.Steps.PageObjects;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class CreateActivitySteps : StepsForPage<CreateActivityPage>
    {
        [Given(@"I am on the 'Create Activity' page")]
        public void GivenIAmOnPage()
        {
            Page.Visit();
        }

        [When(@"I create the following activity")]
        public void CreateActivity(Table table)
        {
            // TODO: Use FillInstance when SpecFlow 1.6 is installed
            var row = table.Rows[0];
            Page.Heading = row["Heading"];
            Page.NumberOfHours = row["NumberOfHours"];
            Page.Date =  row["Date"];
            Page.Coach = row["Coach"];
            Page.Customer = row["Customer"];

            Page.Submit();
        }

        [When(@"I create an activity with '(.*)' set to empty")]
        public void CreateActivityWithFieldEmpty(string fieldName)
        {
            Page.SetDefaultData();
            Page.SetValue(fieldName, string.Empty);
            Page.Submit();
        }


    }
}
