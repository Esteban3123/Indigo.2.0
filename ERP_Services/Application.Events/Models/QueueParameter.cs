using Infrastructure.CrossCutting.Base;

namespace Application.Events.Models
{
    public class QueueParameter
    {

        public string db;

        public bool PublishEvent;

        public string UrlQueue;

        public string Usercode;

        public string action;

        public AuditMessage audit;

    }
}
