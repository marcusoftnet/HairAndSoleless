using System.Collections.Generic;
using System.Linq;
using Specs.EndToEnd.Steps.Infrastructure;

namespace Specs.EndToEnd.Steps.PageObjects
{
    public class CustomersPage : PageObjectBase
    {
        private const string COLNAME_NAME = "Name";

        public CustomersPage() : base(WebBrowser.Current, "/Customers") { }

        public IEnumerable<string> CustomerNames
        {
            get
            {
                return from c in TableCellsById(COLNAME_NAME)
                       select c.Text.Trim();
            }
        }

        public void ClickNewLink()
        {
            ClickLink("Create New");
        }
    }
}
