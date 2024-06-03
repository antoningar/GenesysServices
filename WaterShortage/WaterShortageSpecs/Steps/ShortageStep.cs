namespace ComapniesWaterSpecs.Steps;

[Binding]
public class ShortageStep
{
    [Given(@"SIREN api is down")]
    public void GivenSirenApiIsDown()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"I would like to know if my companies is vulnerable to water shortage in my region")]
    public void WhenIWouldLikeToKnowIfMyCompaniesIsVulnerableToWaterShortageInMyRegion()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"I got a respones (.*)")]
    public void ThenIGotARespones(string responseCode)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"reason is (.*)")]
    public void ThenReasonIs(string reason)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"Water api is down")]
    public void GivenWaterApiIsDown()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"I got a response (.*)")]
    public void ThenIGotAResponse(string responseCode)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"My company operate in (.*)")]
    public void GivenMyCompanyOperateIn(string region)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"There is a a (.*) shortage")]
    public void GivenThereIsAaShortage(string gravity)
    {
        ScenarioContext.StepIsPending();
    }
}