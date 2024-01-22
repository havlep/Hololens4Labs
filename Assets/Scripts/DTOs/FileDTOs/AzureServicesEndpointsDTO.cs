// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.
using System;

namespace HoloLens4Labs.Scripts.DTOs
{
    [Serializable]
    public class AzureServicesEndpointsDTO
    {
        public string ImageAnalysisEndpoint;
        public string ImageAnalysisKey;
        public string ImageAnalysisRegion;
        public string AzureStorageEndpoint;

    }
}