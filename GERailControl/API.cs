using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrainControl
{
    class API
    {
        private HttpClient _client;

        public API()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://railwayapi.bnair.repl.co");
        }

        public async Task<List<Train>> GetTrains()
        {
            var response = await _client.GetAsync("/trains");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Train>>(content);
        }

        public async Task<Train> GetTrain(int trainId)
        {
            var response = await _client.GetAsync($"/get/{trainId}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Train>(content);
        }

        public async Task SendTrainData(Train train)
        {
            var json = JsonConvert.SerializeObject(train);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync($"/send/{train.TrainId}", content);
        }

        public async Task CreateTrain(Train train)
        {
            var json = JsonConvert.SerializeObject(train);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync($"/create/train/{train.TrainId}/{train.X},{train.Y},{train.Z}", content);
        }

        public async Task<List<Command>> GetCommands()
        {
            var response = await _client.GetAsync("/get/commands");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Command>>(content);
        }

        public async Task<Command> GetCommand(int trainId)
        {
            var response = await _client.GetAsync($"/get/commands/{trainId}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Command>(content);
        }

        public async Task CreateStation(Station station)
        {
            var json = JsonConvert.SerializeObject(station);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync($"/create/station/{station.StationId}/{station.Name}/{station.X},{station.Y},{station.Z}", content);
        }

        public async Task<List<Station>> GetStations()
        {
            var response = await _client.GetAsync("/stations");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Station>>(content);
        }

        public async Task CreateTrainSchedule(TrainSchedule trainSchedule)
        {
            var json = JsonConvert.SerializeObject(trainSchedule);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync($"/create/trainschedule/{trainSchedule.TrainId}", content);
        }

        public async Task AddStationToTrainSchedule(int trainId, int stationId)
        {
            await _client.PostAsync($"/addstationtotrainschedule/{trainId}/{stationId}", null);
        }

        public async Task RemoveStationFromTrainSchedule(int trainId, int stationId)
        {
            await _client.PostAsync($"/removestationfromtrainschedule/{trainId}/{stationId}", null);
        }

        public async Task<List<TrainSchedule>> GetTrainSchedules()
        {
            var response = await _client.GetAsync("/trainschedules");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TrainSchedule>>(content);
        }
    }
}
