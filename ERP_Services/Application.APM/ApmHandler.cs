using System;

namespace Application.APM
{
    public interface ApmHandler
    {
        void AddCustomAttribute(string key, object value);
        void Error(string message);
        void Error(Exception exception);
    }
}
