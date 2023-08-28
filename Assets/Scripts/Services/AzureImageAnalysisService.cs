
using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model.DataTypes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class AzureImageAnalysisService
{
    string key;
    string requestParameters = "/computervision/imageanalysis:analyze?features=read&model-version=latest&language=en&api-version=2023-02-01-preview";
    string requestURI;
    private readonly HttpClient client;

    public AzureImageAnalysisService(string key, string endpoint, HttpClient client)
    {
        this.key = key;
        requestURI = endpoint + requestParameters;
        this.client = client;
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

    }

    public async Task<string> Transcribe(ImageData imageData)
    {
        HttpResponseMessage result;
        using (var content = new ByteArrayContent(imageData.Data))
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
