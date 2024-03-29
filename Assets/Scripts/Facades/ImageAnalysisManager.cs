// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Services;
using System.Threading.Tasks;
using HoloLens4Labs.Scripts.DTOs;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Managers
{

    /// <summary>
    /// A facade class that handles the communication with the Azure Image Analysis service
    /// </summary>
    public class ImageAnalysisManager : MonoBehaviour
    {

        [Header("Azure Image Settings")]
        [SerializeField]
        private string subscriptionKey;
        [SerializeField]
        private string endpoint;

        private AzureImageAnalysisService AzureImageAnalysis;

        /// <summary>
        /// Setup the Azure Image Analysis service when the object is created
        /// </summary>
        public void Init(AzureServicesEndpointsDTO azureServicesEndpoints)
        {
            
            if(string.IsNullOrEmpty(subscriptionKey))
                subscriptionKey = azureServicesEndpoints.ImageAnalysisKey;

            if(string.IsNullOrEmpty(endpoint))
                endpoint = azureServicesEndpoints.ImageAnalysisEndpoint;
            
            AzureImageAnalysis = new AzureImageAnalysisService(subscriptionKey, endpoint, new System.Net.Http.HttpClient());
        }

        /// <summary>
        /// Transcribe an image
        /// </summary>
        /// <param name="image">The ImageData of the image that will be transcribed</param>
        /// <exception cref="Exception">Thrown when the request is not successful</exception>"""
        /// <returns>The transcription of the image</returns>
        public Task<string> TranscribeImage(ImageData image)
        {
            var result = AzureImageAnalysis.Transcribe(image);
            return result;
        }

    }
}
