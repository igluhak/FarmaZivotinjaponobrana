using System;
using System.Runtime.Serialization;

namespace FarmaZivotinja.Core
{
    [Serializable]
    public class FarmException : Exception
    {
        public FarmException() { }
        public FarmException(string message) : base(message) { }
        public FarmException(string message, Exception inner) : base(message, inner) { }
    }
}
