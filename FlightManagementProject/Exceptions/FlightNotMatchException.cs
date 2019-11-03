using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.Facade
{
    [Serializable]
    public class FlightNotMatchException : ApplicationException
    {
        public FlightNotMatchException()
        {
        }

        public FlightNotMatchException(string message) : base(message)
        {
        }

        public FlightNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}