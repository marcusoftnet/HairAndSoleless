using TechTalk.SpecFlow;
using WatiN.Core;

namespace Specs.EndToEnd.Steps.Infrastructure
{
    [Binding]
    public class WebBrowser
    {
        private const string BROWSER_KEY = "browser";

        public static Browser Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey(BROWSER_KEY))
                    ScenarioContext.Current[BROWSER_KEY] = new IE();
                return (IE)ScenarioContext.Current[BROWSER_KEY];
            }
        }

        [AfterScenario] // TODO: AfterTestRun?
        public static void Close()
        {
            if (ScenarioContext.Current.ContainsKey(BROWSER_KEY))
                WebBrowser.Current.Close();
        }
    }
}
