using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Services;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;


public class AzureImageAnalysisServiceTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void ImageWithVialSuccess()
    {
        string key = "00aea6c033354ee7b9a3c54e423846a7";
        string endpoint = "https://westeurope.api.cognitive.microsoft.com";

        ImageData imageData = new ImageData("1", DateTime.Now, "1", "1", "testImage");
        imageData.Data = File.ReadAllBytes("Assets\\Tests\\EditModeTests\\TestImages\\GlacialAcetic-GHS.jpg");


        AzureImageAnalysisService service = new AzureImageAnalysisService(key, endpoint, new System.Net.Http.HttpClient());

        // This code here allows us to run the async method in a sync way as nunittests in unity do not support async methods
        var task = Task.Run(async () => { return await service.Transcribe(imageData); });

        Assert.AreEqual(task.Result, "Glacial\nA\ncetic\nacid");

    }

    [Test]
    public void ImageWithMultipleLineTextSuccess()
    {
        string key = "00aea6c033354ee7b9a3c54e423846a7";
        string endpoint = "https://westeurope.api.cognitive.microsoft.com";

        ImageData imageData = new ImageData("1", DateTime.Now, "1", "1", "testImage");
        imageData.Data = File.ReadAllBytes("Assets\\Tests\\EditModeTests\\TestImages\\radomSticky.jpg");

        AzureImageAnalysisService service = new AzureImageAnalysisService(key, endpoint, new System.Net.Http.HttpClient());
        // This code here allows us to run the async method in a sync way as nunittests in unity do not support async methods
        var task = Task.Run(async () => { return await service.Transcribe(imageData); });

        Assert.AreEqual(task.Result, "One small crack\ndoes not mean you\nare broken, it means\nyou were put to the\ntest & you didn't\nfall apart.\nyou're amazing!\noperationbeautiful.com");

    }

}
