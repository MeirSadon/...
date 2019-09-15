using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.DAO
{
    [Serializable]
    public class UserIsAlreadyExistException : ApplicationException
    {
        public UserIsAlreadyExistException()
        {
        }

        public UserIsAlreadyExistException(string message) : base(message)
        {
        }

        public UserIsAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserIsAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}