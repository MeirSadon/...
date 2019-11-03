using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.Facade
{
    [Serializable]
    public class DepartureTimeTooLateException : ApplicationException
    {
        public DepartureTimeTooLateException()
        {
        }

        public DepartureTimeTooLateException(string message) : base(message)
        {
        }

        public DepartureTimeTooLateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DepartureTimeTooLateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}