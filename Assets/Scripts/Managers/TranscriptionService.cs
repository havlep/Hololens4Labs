
using Azure;
using Azure.AI.Vision.Common;
using Azure.AI.Vision.ImageAnalysis;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using System.Net;
using System;
using HoloLens4Labs.Scripts.Model.DataTypes;
using System.Text;
using System.Linq;
using PlasticPipe.PlasticProtocol.Messages;
using System.Threading.Tasks;


namespace HoloLens4Labs.Scripts.Services
{
    public class TranscriptionService
    {

        private string visionEndpoint = default;


        private string visionKey = default;

        VisionServiceOptions serviceOptions = null;

        public TranscriptionService(string visionEndpoint, string visionKey)
        {
            this.visionEndpoint=visionEndpoint;
            this.visionKey=visionKey;
            this.serviceOptions=new VisionServiceOptions(visionEndpoint, new AzureKeyCredential(visionKey));
        }


        public string Transcribe(string imageUrl)
        {

            VisionSource visionSource = null;
            try
            {
                
                visionSource = VisionSource.FromUrl(new Uri(imageUrl));
                /*var analysisOptions = new ImageAnalysisOptions()
                {
                    ModelVersion = "latest",
                    Features = ImageAnalysisFeature.Text,
                    Language = "en"
                };
                */
               /* using (var analyzer = new ImageAnalyzer(serviceOptions, visionSource, analysisOptions))
                {

                    // Implement the Analyzed event to display results or error message
                    //analyzer.Analyzed += AnalysisResultProcessing;
                    return AnalysisResultProcessing(analyzer.Analyze());

                }
               */
                              return "test";
            }
            catch (Exception e)
            {
                throw e;
                //TODO : NotifyUser(e.ToString(), NotifyType.Er
            }
            finally
            {
                if (visionSource != null)
                {
                    visionSource.Dispose();
                }
            }
        }
        private string AnalysisResultProcessing(ImageAnalysisResult result)
        {


            if (result.Reason == ImageAnalysisResultReason.Analyzed)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($" Image height = {result.ImageHeight}");
                sb.AppendLine($" Image width = {result.ImageWidth}");
                sb.AppendLine($" Model version = {result.ModelVersion}");



                if (result.Text != null)
                {
                    sb.AppendLine($" Text:");
                    foreach (var line in result.Text.Lines)
                    {
                        string pointsToString = "{" + string.Join(',', line.BoundingPolygon.Select(p => p.ToString())) + "}";
                        sb.AppendLine($"   Line: '{line.Content}', Bounding polygon {pointsToString}");

                        foreach (var word in line.Words)
                        {
                            pointsToString = "{" + string.Join(',', word.BoundingPolygon.Select(p => p.ToString())) + "}";
                            sb.AppendLine($"     Word: '{word.Content}', Bounding polygon {pointsToString}, Confidence {word.Confidence:0.0000}");
                        }
                    }
                }

                var resultDetails = ImageAnalysisResultDetails.FromResult(result);
                sb.AppendLine($" Result details:");
                sb.AppendLine($"   Image ID = {resultDetails.ImageId}");
                sb.AppendLine($"   Result ID = {resultDetails.ResultId}");
                sb.AppendLine($"   Connection URL = {resultDetails.ConnectionUrl}");
                sb.AppendLine($"   JSON result = {resultDetails.JsonResult}");

                return sb.ToString();
                //TODO : NotifyUser(sb.ToString(), NotifyType.StatusMessage);

            }
            else // result.Reason == ImageAnalysisResultReason.Error
            {
                var errorDetails = ImageAnalysisErrorDetails.FromResult(result);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" Analysis failed.");
                sb.AppendLine($"   Error reason : {errorDetails.Reason}");
                sb.AppendLine($"   Error code : {errorDetails.ErrorCode}");
                sb.AppendLine($"   Error message: {errorDetails.Message}");
                return sb.ToString();

            }


        }

    }
}
