using Specs.EndToEnd.Steps.Infrastructure;

namespace Specs.EndToEnd.Steps.PageObjects
{
    public class CreateCoachPage : PageObjectBase
    {
        private const string EMAIL_FIELD = "Email";
        private const string NAME_FIELD = "Name";
        private const string TEAM_FIELD = "Team";

        private const string NAME_DEFAULT = "Test Coach";
        private const string EMAIL_DEFAULT = "test.coach@avega.se";
        private const string TEAM_DEFAULT = "Test Team";


        public CreateCoachPage() : base(WebBrowser.Current, "/Coaches/Create") { }

        public string EmailField
        {
            set {SetValue(EMAIL_FIELD, value); }
        }

        public string NameField
        {
            set { SetValue(NAME_FIELD, value); }
        }

        public string TeamField
        {
            set { SetValue(TEAM_FIELD, value); }
        }

        public void Submit()
        {
            ClickButton("Create");
        }

        public void SetDefaultData()
        {
            NameField = NAME_DEFAULT;
            EmailField = EMAIL_DEFAULT;
            TeamField = TEAM_DEFAULT;
        }
    }
}
