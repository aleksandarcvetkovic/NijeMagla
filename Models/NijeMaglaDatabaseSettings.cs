namespace Nije_Magla_API
{
    public class NijeMaglaDatabaseSettings: INijeMaglaDatabaseSettings
    {
        public string SensorCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
