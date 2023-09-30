using System.Collections.Generic;

namespace TrainControl
{
    class TrainSchedule
    {
        public int TrainId { get; set; }
        public List<int> Stations { get; set; }

        public void AddStation(int stationId)
        {
            Stations.Add(stationId);
        }

        public void RemoveStation(int stationId)
        {
            Stations.Remove(stationId);
        }
    }
}