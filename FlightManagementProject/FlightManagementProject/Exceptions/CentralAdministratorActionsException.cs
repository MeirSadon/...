using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.Facade
{
    [Serializable]
    public class CentralAdministratorException : ApplicationException
    {
        public CentralAdministratorException()
        {
        }

        public CentralAdministratorException(string message) : base(message)
        {
        }

        public CentralAdministratorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CentralAdministratorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}