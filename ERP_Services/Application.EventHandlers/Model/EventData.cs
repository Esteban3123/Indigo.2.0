using Newtonsoft.Json;
using static Application.EventHandlers.Enums.Enums;

namespace Application.EventHandlers.Model
{
    public class EventData
    {
        public EventData(
            object _data,
            string _source,
            string _action,
            string _db,
            string _userCode,
            int _timestamp)
        {
            data = _data;
            source = _source;
            action = _action;
            db = _db;
            timestamp = _timestamp;
            user_code = _userCode;
        }

        public object data { get; set; }
        public string source { get; set; }
        public string action { get; set; }
        public string db { get; set; }
        public string user_code { get; set; }
        public int timestamp { get; set; }
    }
}
