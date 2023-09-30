using System;

namespace TrainControl
{
    class Train
    {
        public int TrainId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public DateTime Timestamp { get; set; }

        public double CalculateSpeed(Train previous)
        {
            var distance = Math.Sqrt(Math.Pow(X - previous.X, 2) + Math.Pow(Y - previous.Y, 2) + Math.Pow(Z - previous.Z, 2));
            var time = (Timestamp - previous.Timestamp).TotalSeconds;

            return distance / time * 3.6;
        }

        public void UpdatePosition(double x, double y, double z, DateTime timestamp)
        {
            X = x;
            Y = y;
            Z = z;
            Timestamp = timestamp;
        }
    }
}