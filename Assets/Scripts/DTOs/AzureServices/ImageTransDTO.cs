using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloLens4Labs.Scripts.DTOs
{

    public class ReadResultDTO
    { 

        public string content { get; set; }

    }
    public class ImageTransDTO 
    {
        public ReadResultDTO readResult { get; set; }

    }
}