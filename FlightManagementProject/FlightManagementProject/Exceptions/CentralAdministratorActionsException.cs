using System;
using System.Runtime.Serialization;

namespace FlightManagementProject.Facade
{
    [Serializable]
    public class CentralAdministratorActionsException : ApplicationException
    {
        public CentralAdministratorActionsException()
        {
        }

        public CentralAdministratorActionsException(string message) : base(message)
        {
        }

        public CentralAdministratorActionsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CentralAdministratorActionsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}