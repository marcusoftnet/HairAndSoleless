using Specs.EndToEnd.Steps.PageObjects;
using TechTalk.SpecFlow;

namespace Specs.EndToEnd.Steps
{
    public abstract class PageSteps<T> where T: PageObjectBase, new()
    {
        protected PageSteps()
        {
            ScenarioContext.Current.Set(new T());
        }

        protected static T Page { get { return ScenarioContext.Current.Get<T>(); } }   
    }
}