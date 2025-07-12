using System;

namespace Systems.EventBus.Data
{
    public struct EventLog
    {
        public DateTime Time; 
        public object Value;
    }
}