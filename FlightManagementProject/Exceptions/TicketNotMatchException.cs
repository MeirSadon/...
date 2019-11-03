using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.Facade
{
    [Serializable]
    public class TicketNotMatchException : ApplicationException
    {
        public TicketNotMatchException()
        {
        }

        public TicketNotMatchException(string message) : base(message)
        {
        }

        public TicketNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TicketNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}