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
                if (!FeatureContext.Current.ContainsKey(BROWSER_KEY))
                    FeatureContext.Current[BROWSER_KEY] = new IE();
                return (IE)FeatureContext.Current[BROWSER_KEY];
            }
        }

        [AfterFeature]
        public static void Close()
        {
            if (FeatureContext.Current.ContainsKey(BROWSER_KEY))
                Current.Close();
        }
    }
}
