using System;
using System.Collections.Generic;
using System.Linq;
using Specs.EndToEnd.Steps.Infrastructure;

namespace Specs.EndToEnd.Steps.PageObjects
{
    public class ActivitiesPage : PageObjectBase
    {
        public ActivitiesPage() : base(WebBrowser.Current, "/Activities") { }

        private const string COLNAME_HEADING = "Heading";


        public IEnumerable<string> ActivitiyHeadings
        {
            get
            {
                return from c in TableCellsById(COLNAME_HEADING)
                       select c.Text.Trim();
            }
        }

        public void GotoNewActivity()
        {
            ClickLink("Create New");
        }
    }
}