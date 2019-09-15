using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.Facade
{
    [Serializable]
    public class UserNotExistException : ApplicationException
    {
        public UserNotExistException()
        {
        }

        public UserNotExistException(string message) : base(message)
        {
        }

        public UserNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}