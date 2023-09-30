using System;

namespace TrainControl
{
    class Command
    {
        public int TrainId { get; set; }
        public double Throttle { get; set; }
        public double Brake { get; set; }
        public DateTime Timestamp { get; set; }
    }
}