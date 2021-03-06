// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.5.0.0
//      Runtime Version:4.0.30319.225
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace Specs.EndToEnd.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.5.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Create new activity")]
    public partial class CreateNewActivityFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CreateNewActivity.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Create new activity", "In order to keep track of the different acitivites that coaches has made\nAs an Av" +
                    "ega Coach\nI want to be able to create a new activity", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Navigate to the new activity page")]
        public virtual void NavigateToTheNewActivityPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Navigate to the new activity page", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("I am on the \'Activities\' page");
#line 8
 testRunner.When("I choose to create new activity");
#line 9
 testRunner.Then("I should be on the \'Create Activity\' page");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Creating new activity without validation errors")]
        public virtual void CreatingNewActivityWithoutValidationErrors()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating new activity without validation errors", ((string[])(null)));
#line 11
this.ScenarioSetup(scenarioInfo);
#line 12
 testRunner.Given("there are no activites for coach \'Test Coach\' in the database");
#line 13
  testRunner.And("there is a coach named \'Test Coach\' in the database");
#line 14
  testRunner.And("there is a customer named \'Test Customer\' in the database");
#line 15
  testRunner.And("I am on the \'Create Activity\' page");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Heading",
                        "NumberOfHours",
                        "Date",
                        "Coach",
                        "Customer"});
            table1.AddRow(new string[] {
                        "BDD Workshop",
                        "12",
                        "2011/01/01",
                        "Test Coach",
                        "Test Customer"});
#line 16
 testRunner.When("I create the following activity", ((string)(null)), table1);
#line 19
 testRunner.Then("I should be on the \'Activities\' page");
#line 20
  testRunner.And("an activity with the heading \'BDD Workshop\' should be in the activities list");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Creating new activity with missing values should give validation error")]
        [NUnit.Framework.TestCaseAttribute("Heading", "Heading", new string[0])]
        [NUnit.Framework.TestCaseAttribute("CoachId", "Coach", new string[0])]
        [NUnit.Framework.TestCaseAttribute("CustomerId", "Customer", new string[0])]
        [NUnit.Framework.TestCaseAttribute("Date", "Date", new string[0])]
        [NUnit.Framework.TestCaseAttribute("NumberOfHours", "Number of hours", new string[0])]
        public virtual void CreatingNewActivityWithMissingValuesShouldGiveValidationError(string field, string errorMessage, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating new activity with missing values should give validation error", exampleTags);
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("I am on the \'Create Activity\' page");
#line 24
 testRunner.When(string.Format("I create an activity with \'{0}\' set to empty", field));
#line 25
 testRunner.Then(string.Format("a required field validation error for \'{0}\' should be displayed", errorMessage));
#line 26
  testRunner.And("I should still be on the \'Create Activity\' page");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
