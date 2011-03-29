using System;
using System.Collections.Generic;
using System.Linq;
using Specs.EndToEnd.Steps.Infrastructure;

namespace Specs.EndToEnd.Steps.PageObjects
{
    public class CoachesPage: PageObjectBase
    {
        public CoachesPage() : base(WebBrowser.Current, "/Coaches") { }

        private const string COLNAME_NAME = "Name";
        
        public IEnumerable<string> CoachNames
        {
            get
            {
                return from c in TableCellsById(COLNAME_NAME)
                       select c.Text.Trim();
            }
        }

        public void GotoNewCoach()
        {
            ClickLink("Create New");
        }
    }
}
