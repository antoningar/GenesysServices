using Api.WaterShortage.Models;
using Api.WaterShortage.Services;
using Moq;
using Xunit;

namespace WeatherShortageSpecs.Steps;

[Binding]
public class ShortageSteps
{
    private readonly WaterShortageApiFactory _waterShortageApi = new();
    private HttpResponseMessage? _response; 
    
    [Given(@"SIRET api is down")]
    public void GivenSiretApiIsDown()
    {
        Mock<ISiretService> mockService = new();
        mockService
            .Setup(s => s.GetCodeRegionBySiretAsync(It.IsAny<string>()))
            .Throws(new Exception("External SIRET service down"));
        _waterShortageApi.SiretService = mockService.Object;
    }

    [When(@"I would like to know if my companies is vulnerable to water shortage in my region")]
    public async Task WhenIWouldLikeToKnowIfMyCompaniesIsVulnerableToWaterShortageInMyRegion()
    {
        HttpClient client = _waterShortageApi.CreateDefaultClient(new Uri("http://localhost/"));
        _response = await client.GetAsync("/api/v1/watershortage?siret=972105410");
    }

    [Given(@"Water api is down")]
    public void GivenWaterApiIsDown()
    {
        Mock<IWaterService> mockService = new();
        mockService
            .Setup(s => s.GetRegionGravityByCodeAsync(It.IsAny<string>()))
            .Throws(new Exception("External water service down"));
        _waterShortageApi.WaterService = mockService.Object;
    }

    [Given(@"My company operate in (.*)")]
    public void GivenMyCompanyOperateIn(string postalCode)
    {
        Mock<ISiretService> mockService = new();
        mockService
            .Setup(s => s.GetCodeRegionBySiretAsync(It.IsAny<string>()))
            .Returns(Task.FromResult("972105410"));
        _waterShortageApi.SiretService = mockService.Object;
    }

    [Given(@"There is a a (.*) shortage")]
    public void GivenThereIsAaShortage(string gravity)
    {
        Mock<IWaterService> mockService = new();
        mockService
            .Setup(s => s.GetRegionGravityByCodeAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(new ShortageResponse("Pyrénées-Atlantiques", true, gravity)));
        _waterShortageApi.WaterService = mockService.Object;
    }

    [Then(@"I got a response (.*)")]
    public void ThenIGotAResponse(int responseCode)
    {
        Assert.Equal(responseCode, (int)_response!.StatusCode);
    }
}