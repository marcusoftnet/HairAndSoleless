using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WatiN.Core;

namespace Specs.EndToEnd.Steps.PageObjects
{
    public abstract class PageObjectBase
    {
        protected readonly Browser Browser;
        private readonly string relativeUrl;

        protected PageObjectBase(Browser browser, string relativeUrl)
        {
            Browser = browser;
            this.relativeUrl = relativeUrl;
        }

        public void Visit()
        {
            var rootUrl = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
            var absoluteUrl = new Uri(rootUrl, relativeUrl);
            Browser.GoTo(absoluteUrl);
        }

        protected void ClickButton(string buttontext)
        {
            Browser.Button(x => x.Text == buttontext).Click();
        }

        protected void ClickLink(string linkText)
        {
            Browser.Link(x => x.Text == linkText).Click();
        }

        public string Title
        {
            get { return Browser.Title; }
        }

        protected void SetValue(string name, string value)
        {
            var textField = Browser.TextField(Find.ByName(name));
            if (textField.Exists)
            {
                textField.Value = value;
                return;
            }

            var select = Browser.SelectList(Find.ByName(name));
            if (select.Exists)
            {
                select.Select(value);
                return;
            }

            throw new InvalidOperationException("Could not find a HTML Element by the name " + name);
        }

        protected string GetValue(string name)
        {
            var textField = Browser.TextField(Find.ByName(name));
            if (textField.Exists)
            {
                return textField.Value;
            }

            var select = Browser.SelectList(Find.ByName(name));
            if (select.Exists)
            {
                return select.GetAttributeValue("selected");
            }

            throw new InvalidOperationException("Could not find a HTML Element by the name " + name);
        }

        protected IEnumerable<TableCell> TableCellsById(string cellId)
        {
            return from c in Browser.TableCells.Filter(x => x.Id == cellId)
                   select c;
        }

        public bool ValidationErrorExistsFor(string fieldWithError)
        {
            var spans = from s in Browser.Spans
                        where s.GetAttributeValue("data-valmsg-for") == fieldWithError
                        select s;
            
            return spans.Count() == 1;
        }
    }
}
