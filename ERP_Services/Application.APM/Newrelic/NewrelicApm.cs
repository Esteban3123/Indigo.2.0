using NewRelic.Api.Agent;
using System;
using System.Collections.Generic;

namespace Application.APM.Newrelic
{
    public class NewrelicApm : ApmHandler
    {
        private ITransaction transaction;

        public NewrelicApm()
        {
            IAgent agent = NewRelic.Api.Agent.NewRelic.GetAgent();

            if (agent != null) transaction = agent.CurrentTransaction;
        }

        private void CheckTransaction()
        {
            //IAgent agent = NewRelic.Api.Agent.NewRelic.GetAgent();

            //if (agent != null) transaction = agent.CurrentTransaction;
        }

        public void AddCustomAttribute(string key, object value)
        {
            CheckTransaction();

            if (transaction != null) transaction.AddCustomAttribute(key, value);
        }

        public void Error(string message)
        {
            CheckTransaction();

            var dict = new Dictionary<string, string>();
            NewRelic.Api.Agent.NewRelic.NoticeError(message, dict);
        }

        public void Error(Exception exception)
        {
            CheckTransaction();

            var dict = new Dictionary<string, string>();
            NewRelic.Api.Agent.NewRelic.NoticeError(exception, dict);
        }
    }
}
