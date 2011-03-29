using Should.Fluent;
using Specs.EndToEnd.Steps.Infrastructure;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    [Binding]
    public class GenericBrowserSteps
    {
        [Then(@"a validation error for '(.*)' should be displayed")]
        public void ValidationErrorForField(string fieldName)
        {
            WebBrowser.Current.ValidationErrorExistsFor(fieldName).Should().Not.Be.False();
        }

    }
}
