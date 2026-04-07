using Allure.Net.Commons;
using Allure.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RickAndMortyTests.Helpers;
using System.Text;



namespace RickAndMortyTests.Tests;

/// <summary>
/// Base class for all API tests.
/// Contains common setup and teardown logic.
/// 
/// EXERCISE TODO:
/// In Part 2, you will add Allure auto-attachment logic to TearDown.
/// </summary>
[AllureNUnit]
public class BaseTest
{
    protected ApiClient _apiClient = null!;

    [SetUp]
    public void Setup()
    {
        _apiClient = new ApiClient();
    }

    [TearDown]
    public void TearDown()
    {
        // EXERCISE TODO (Part 2):
        // 1. Check if test failed using TestContext.CurrentContext.Result.Outcome.Status
        // 2. If failed AND _apiClient.LastResponseBody is not empty:
        //    - Use AllureApi.AddAttachment() to attach the response body
        //    - Name: "response-body.json"
        //    - Type: "application/json"
        //    - Content: System.Text.Encoding.UTF8.GetBytes(_apiClient.LastResponseBody)
        // 
        // This is the "killer feature" - automatic response capture on test failure!
        var status = TestContext.CurrentContext.Result.Outcome.Status; 
        
        if (status == TestStatus.Failed && !string.IsNullOrWhiteSpace(_apiClient.LastResponseBody)) 
        { 
            AllureApi.AddAttachment("response-body", 
                                    "application/json", 
                                    Encoding.UTF8.GetBytes(_apiClient.LastResponseBody), 
                                    "json"); 
        }
        _apiClient.Dispose();
    }
}
