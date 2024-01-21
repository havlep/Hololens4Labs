// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.
using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model.DataTypes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;


namespace HoloLens4Labs.Scripts.Services
{
    /// <summary>
    /// Class that handles the communication with the Azure Image Analysis service around transcription
    /// </summary>
    public class AzureImageAnalysisService
    {

        private string requestParameters = "/computervision/imageanalysis:analyze?features=read&model-version=latest&language=en&api-version=2023-02-01-preview";
        private string requestURI;
        private readonly HttpClient client;

        /// <summary>
        /// Constructor that setups the request URI and the HttpClient
        /// </summary>
        /// <param name="key">The key that will be used to authenticate the request</param>
        /// <param name="endpoint">The endpoint that will be used to make the request</param>
        /// <param name="client">The HttpClient that will be used to make the request</param>
        /// <exception cref="Exception">Thrown when the request URI is not valid</exception>"
        public AzureImageAnalysisService(string key, string endpoint, HttpClient client)
        {

            requestURI = endpoint + requestParameters;
            this.client = client;
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

        }

        /// <summary>
        /// A method that sends a request to the Azure Image Analysis service to transcribe an image present in ImageData
        /// </summary>
        /// <param name="imageData">The ImageData that contains the image that will be transcribed</param>
        /// <returns>The transcription of the image</returns>
        /// <exception cref="Exception">Thrown when the request is not successful</exception>""
        public async Task<string> Transcribe(ImageData imageData)
        {
            HttpResponseMessage result;

            var data = await imageData.getData();

            if (data == null)
            {
                Debug.LogError("Error in AzureImageAnalysisService: ImageData does not contain any image data");
                throw new Exception("Error in AzureImageAnalysisService: ImageData does not contain any image data");
            }

            using (var content = new ByteArrayContent(data))
            {


                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                result = await client.PostAsync(requestURI, content);
            }

            string resultString = await result.Content.ReadAsStringAsync();


            if (result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK)
            {


                return JsonConvert.DeserializeObject<ImageTransDTO>(resultString).readResult.content;


            }
            else
            {
                Debug.LogError("Error in AzureImageAnalysisService: " + result.StatusCode + " " + result.ReasonPhrase);
                throw new Exception("Error in AzureImageAnalysisService: " + result.StatusCode + " " + result.ReasonPhrase);
            }
        }
    }
}