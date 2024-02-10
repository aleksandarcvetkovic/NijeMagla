﻿
using MongoDB.Driver;

namespace Nije_Magla_API
{
    public class NijeMaglaService : INijeMaglaService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Sensor> _senzori;

        public NijeMaglaService(INijeMaglaDatabaseSettings settings, IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(settings.DatabaseName);
            _senzori = _database.GetCollection<Sensor>(settings.SensorCollectionName);

        }
        public void AddMeasurement(string id, Measurement value)
        {
             var _merenja = _database.GetCollection<Measurement>(id);
            _merenja.InsertOne(value);
        }

        public Sensor CreateSensor(Sensor sensor)
        {
            _senzori.InsertOne(sensor);
            return sensor;
        }

        public void DeleteSensorData(string id)
        {
            _database.DropCollection(id);
        }

        public List<Sensor> GetAllSensors()
        {
            return _senzori.Find(sensor => true).ToList();
        }

        public int GetAverageMeasurement(string id)
        {
            var _merenja = _database.GetCollection<Measurement>(id);
            return (int)_merenja.AsQueryable().Average(doc => doc.Value);
        }

        public Sensor GetSensor(string id)
        {
            return _senzori.Find(sensor => sensor.Id == id).FirstOrDefault();
        }

        public Measurement? ReadMeasurement(string id)
        {
            var _merenja = _database.GetCollection<Measurement>(id);
            return _merenja.AsQueryable().OrderByDescending(doc => doc.Time).FirstOrDefault();
        }

        public void RemoveSensor(string id)
        {
            _senzori.DeleteOne(sensor => sensor.Id == id);
            this.DeleteSensorData(id);
        }

        public void UpdateSensor(string id, Sensor sensor)
        {
            _senzori.ReplaceOne(sensor => sensor.Id == id, sensor);
        }
    }
}