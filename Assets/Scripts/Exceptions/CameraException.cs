using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Exceptions
{
    public class CameraException : Exception
{
    public CameraException() : base() { }
    public CameraException(string message) : base(message) { }
    public CameraException(string message, Exception inner) : base(message, inner) { }
}
}
