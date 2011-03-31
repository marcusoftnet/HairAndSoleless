using System;
using Specs.EndToEnd.Steps.Infrastructure;

namespace Specs.EndToEnd.Steps.PageObjects
{
    public class CreateActivityPage : PageObjectBase
    {
        private const string CUSTOMER_FIELD = "CustomerId";
        private const string COACH_FIELD = "CoachId";
        private const string DATE_FIELD = "Date";
        private const string NUMBEROFHOURS_FIELD = "NumberOfHours";
        private const string HEADING_FIELD = "Heading";

        private const string CUSTOMER_DEFAULT = "EKN";
        private const string COACH_DEFAULT = "Marcus";
        private string DATE_DEFAULT = DateTime.Now.ToShortDateString();
        private const string NUMBEROFHOURS_DEFAULT = "8";
        private const string HEADING_DEFAULT = "Test heading";

        public CreateActivityPage() : base(WebBrowser.Current, "/Activities/Create") { }

        public string Heading
        {
            set { SetValue(HEADING_FIELD, value); }
        }

        public string NumberOfHours
        {
            set { SetValue(NUMBEROFHOURS_FIELD, value); }
        }

        public string Date
        {
            set { SetValue(DATE_FIELD, value); }
        }

        public string Coach
        {
            set { SetValue(COACH_FIELD, value); }
        }

        public string Customer
        {
            set { SetValue(CUSTOMER_FIELD, value); }
        }

        public void Submit()
        {
            ClickButton("Create");
        }

        public void SetDefaultData()
        {
            Customer = CUSTOMER_DEFAULT;
            Coach = COACH_DEFAULT;
            Heading = HEADING_DEFAULT;
            NumberOfHours = NUMBEROFHOURS_DEFAULT;
            Date = DATE_DEFAULT;
        }
    }
}