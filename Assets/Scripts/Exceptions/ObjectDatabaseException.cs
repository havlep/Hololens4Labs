
/* Unmerged change from project 'Scripts.Player'
Before:
using System.Collections;
using System.Collections.Generic;

using System;
After:
using System;
using System.Collections;
using System.Collections.Generic;
*/
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
