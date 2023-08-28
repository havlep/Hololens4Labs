
using HoloLens4Labs.Scripts.Model.DataTypes;
using System.Threading.Tasks;
using UnityEngine;


namespace HoloLens4Labs.Scripts.Managers
{


    public class ImageAnalysisManager : MonoBehaviour
    {

        [Header("Azure Image Settings")]
        [SerializeField]
        private string subscriptionKey;
        [SerializeField]
        private string endpoint;

        private AzureImageAnalysisService AzureImageAnalysis ;
        private void Awake()
        {
            AzureImageAnalysis = new AzureImageAnalysisService(subscriptionKey, endpoint, new System.Net.Http.HttpClient());
        }

        public Task<string> TranscribeImage(ImageData image)
        {
            var result = AzureImageAnalysis.Transcribe(image);
            return result;
        }
        
    }
}
