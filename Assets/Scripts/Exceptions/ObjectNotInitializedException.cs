// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System;

namespace HoloLens4Labs.Scripts.Exceptions
{
    public class ObjectNotInitializedException : Exception
    {
        public ObjectNotInitializedException() : base() { }
        public ObjectNotInitializedException(string message) : base(message) { }
        public ObjectNotInitializedException(string message, Exception inner) : base(message, inner) { }
    }
}

