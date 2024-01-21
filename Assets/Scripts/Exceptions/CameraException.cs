// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System;

namespace HoloLens4Labs.Scripts.Exceptions
{
    public class CameraException : Exception
    {
        public CameraException() : base() { }
        public CameraException(string message) : base(message) { }
        public CameraException(string message, Exception inner) : base(message, inner) { }
    }
}
