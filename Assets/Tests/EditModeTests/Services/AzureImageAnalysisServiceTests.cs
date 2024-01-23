// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Services;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Exceptions;
using UnityEngine;


public class AzureImageAnalysisServiceTests
{

    AzureServicesEndpointsDTO azureServicesEndpoints = default;

    [SetUp]
    public void Init()
    {

        var textAsset = Resources.Load<TextAsset>("AzureServicesEndpointsConfig");
        if (textAsset == null)
        {
            Debug.LogError("AzureServicesEndpointsConfig.json not found in Resources folder");
            throw new ObjectNotInitializedException("Azure services endpoint missing");
        }
        Debug.Log("AzureServicesEndpoints.json loaded from Resources folder" + textAsset.text);
        azureServicesEndpoints = JsonUtility.FromJson<AzureServicesEndpointsDTO>(textAsset.text);


    }

    // A Test behaves as an ordinary method
    [Test]
    public void ImageWithVialSuccess()
    {
        string key = azureServicesEndpoints.ImageAnalysisKey;
        string endpoint = azureServicesEndpoints.ImageAnalysisEndpoint;

        ImageData imageData = new ImageData("1", DateTime.Now, "1", "1",
            _ => Task.FromResult(File.ReadAllBytes("Assets\\Tests\\EditModeTests\\TestImages\\GlacialAcetic-GHS.jpg")));


        AzureImageAnalysisService service = new AzureImageAnalysisService(key, endpoint, new System.Net.Http.HttpClient());

        // This code here allows us to run the async method in a sync way as nunittests in unity do not support async methods
        var task = Task.Run(async () => { return await service.Transcribe(imageData); });

        Assert.AreEqual(task.Result, "Glacial\nA\ncetic\nacid");

    }

    [Test]
    public void ImageWithMultipleLineTextSuccess()
    {
        string key = azureServicesEndpoints.ImageAnalysisKey;
        string endpoint = azureServicesEndpoints.ImageAnalysisEndpoint;

        ImageData imageData = new ImageData("1", DateTime.Now, "1", "1",
            _=> Task.FromResult(File.ReadAllBytes("Assets\\Tests\\EditModeTests\\TestImages\\radomSticky.jpg")));

        AzureImageAnalysisService service = new AzureImageAnalysisService(key, endpoint, new System.Net.Http.HttpClient());
        // This code here allows us to run the async method in a sync way as nunittests in unity do not support async methods
        var task = Task.Run(async () => { return await service.Transcribe(imageData); });

        Assert.AreEqual(task.Result, "One small crack\ndoes not mean you\nare broken, it means\nyou were put to the\ntest & you didn't\nfall apart.\nyou're amazing!\noperationbeautiful.com");

    }

}
