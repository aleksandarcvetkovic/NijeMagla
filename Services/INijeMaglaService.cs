namespace Nije_Magla_API
{
    public interface INijeMaglaService
    {
        List<Sensor> GetAllSensors();
        Sensor GetSensor(string id);
        Sensor CreateSensor(Sensor sensor);
        void UpdateSensor(string id, Sensor sensor);
        void RemoveSensor(string id);
        void AddMeasurement(string id, Measurement value);
        Measurement? ReadMeasurement(string id);

        int GetAverageMeasurement(string id);
        void DeleteSensorData(string id);

        List<PolutionListItem> GetSortedPolution();
    }
}
