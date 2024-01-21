// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System;



namespace HoloLens4Labs.Scripts.Exceptions
{
    public class ObjectDataBaseException : Exception
    {
        public ObjectDataBaseException() : base() { }
        public ObjectDataBaseException(string message) : base(message) { }
        public ObjectDataBaseException(string message, Exception inner) : base(message, inner) { }
    }
}
