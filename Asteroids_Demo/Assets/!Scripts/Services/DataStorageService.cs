namespace Services
{
    public class DataStorageService 
    {
        private IDataStorage _dataStorage;
        public DataStorageService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public int GetInt(string name) => _dataStorage.GetInt(name);
        public void SetInt(string name, int value) => _dataStorage.SaveInt(name, value);
    }
}