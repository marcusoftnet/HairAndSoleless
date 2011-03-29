using System;
using System.Collections.Generic;
using System.Linq;
using WatiN.Core;

namespace Specs.EndToEnd.Steps.Infrastructure
{
    public static class BrowserExtensions
    {
        public static void ClickButton(this Browser browser, string buttontext)
        {
            browser.Button(x => x.Text == buttontext).Click();
        }

        public static void ClickLink(this Browser browser, string linkText)
        {
            browser.Link(x => x.Text == linkText).Click();
        }

        public static void SetValue(this Browser browser, string name, string value)
        {
            var textField = browser.TextField(Find.ByName(name));
            if (textField.Exists)
            {
                textField.Value = value;
                return;
            }

            var select = browser.SelectList(Find.ByName(name));
            if (select.Exists)
            {
                select.Select(value);
                return;
            }

            throw new InvalidOperationException("Could not find a HTML Element by the name " + name);
        }

        public static IEnumerable<TableCell> TableCellsById(this Browser browser, string cellId)
        {
            return from c in browser.TableCells.Filter(x => x.Id == cellId)
                   select c;
        }

        public static bool ValidationErrorExistsFor(this Browser browser, string fieldWithError)
        {
            // TODO: Enabled?
            return browser.Spans.Count(s => s.Enabled &&  
                s.GetAttributeValue("data-valmsg-for") == fieldWithError) == 1;
        }

    }
}
