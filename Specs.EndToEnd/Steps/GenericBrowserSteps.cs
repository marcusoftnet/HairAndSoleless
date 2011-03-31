using Should.Fluent;
using Specs.EndToEnd.Steps.Infrastructure;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class GenericBrowserSteps
    {
        private const string REQUIRED_TEMPLATE = "The {0} field is required.";

        [Then(@"a required field validation error for '(.*)' should be displayed")]
        public void RequiredFieldValidationErrorForField(string fieldName)
        {
            var textToLookFor = string.Format(REQUIRED_TEMPLATE, fieldName);
            WebBrowser.Current.ContainsText(textToLookFor).Should().Be.True();
        }

        [Then(@"I should be on the '(.*)' page")]
        [Then(@"I should still be on the '(.*)' page")]
        public void IShouldBeOnPage(string pageTitle)
        {
            WebBrowser.Current.Title.Should().Equal(pageTitle); 
        }
    }
}
