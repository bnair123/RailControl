using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainControl
{
    class TrainController
    {
        private API _api;
        private List<Train> _trains;
        private List<Station> _stations;
        private List<TrainSchedule> _trainSchedules;

        public TrainController(API api)
        {
            _api = api;
            _trains = new List<Train>();
            _stations = new List<Station>();
            _trainSchedules = new List<TrainSchedule>();
        }

        public async Task UpdateTrains()
        {
            _trains = await _api.GetTrains();
        }

        public async Task UpdateStations()
        {
            _stations = await _api.GetStations();
        }

        public async Task UpdateTrainSchedules()
        {
            _trainSchedules = await _api.GetTrainSchedules();
        }

        public async Task MaintainSpeed()
        {
            foreach (var train in _trains)
            {
                var previous = _trains.FirstOrDefault(t => t.TrainId == train.TrainId);
                if (previous != null)
                {
                    var speed = train.CalculateSpeed(previous);
                    if (speed > 80)
                    {
                        var command = new Command { TrainId = train.TrainId, Throttle = 0, Brake = 0.1, Timestamp = DateTime.UtcNow };
                        await _api.SendTrainData(train);
                    }
                    else if (speed < 80)
                    {
                        var command = new Command { TrainId = train.TrainId, Throttle = 0.1, Brake = 0, Timestamp = DateTime.UtcNow };
                        await _api.SendTrainData(train);
                    }
                }
            }
        }

        public async Task StopTrains()
        {
            foreach (var train in _trains)
            {
                var command = new Command { TrainId = train.TrainId, Throttle = 0, Brake = 1, Timestamp = DateTime.UtcNow };
                await _api.SendTrainData(train);
            }
        }

        public List<Train> GetTrains()
        {
            return _trains;
        }

        public List<Station> GetStations()
        {
            return _stations;
        }

        public List<TrainSchedule> GetTrainSchedules()
        {
            return _trainSchedules;
        }
    }
}
