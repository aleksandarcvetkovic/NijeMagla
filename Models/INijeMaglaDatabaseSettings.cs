namespace Nije_Magla_API
{
    public interface INijeMaglaDatabaseSettings
    {
        string SensorCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
