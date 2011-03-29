using Specs.EndToEnd.Steps.PageObjects;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    public abstract class StepsForPage<T> where T : PageObjectBase, new()
    {
        protected StepsForPage()
        {
            ScenarioContext.Current.Set(new T());
        }

        protected static T Page { get { return ScenarioContext.Current.Get<T>(); } }

    }
}