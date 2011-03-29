using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Specs.EndToEnd.Steps.Infrastructure;
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
            Browser.ClickButton(buttontext);
        }

        protected void ClickLink(string linkText)
        {
            Browser.ClickLink(linkText);
        }

        public string Title
        {
            get { return Browser.Title; }
        }

        public void SetValue(string name, string value)
        {
            Browser.SetValue(name, value);
        }

        protected IEnumerable<TableCell> TableCellsById(string cellId)
        {
            return Browser.TableCellsById(cellId);
        }
    }
}
