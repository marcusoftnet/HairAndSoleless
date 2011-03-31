using Specs.EndToEnd.Steps.Infrastructure;

namespace Specs.EndToEnd.Steps.PageObjects
{
    public class CustomersCreatePage : PageObjectBase
    {
        private const string NAME_FIELD = "Name";
        private const string CONTACT_FIELD = "Contact";

        private const string DEFAULT_NAME = "Testing Inc.";
        private const string DEFAULT_CONTACT = "test@testinginc.com";

        public CustomersCreatePage() : base(WebBrowser.Current, "/Customers/Create") { }
        
        public string NameField
        {
            set { SetValue(NAME_FIELD, value); }
        }

        public string ContactField
        {
            set { SetValue(CONTACT_FIELD, value); }
        }

        public void Submit() { ClickButton("Create"); }

        public void SetDefaultData()
        {
            NameField = DEFAULT_NAME;
            ContactField = DEFAULT_CONTACT;
        }
    }
}
