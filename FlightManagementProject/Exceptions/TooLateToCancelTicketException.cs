using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.Facade
{
    [Serializable]
    public class TooLateToCancelTicketException : ApplicationException
    {
        public TooLateToCancelTicketException()
        {
        }

        public TooLateToCancelTicketException(string message) : base(message)
        {
        }

        public TooLateToCancelTicketException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooLateToCancelTicketException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}