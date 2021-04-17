namespace Services
{
    public interface IDataStorage
    {
        public void SaveInt(string name, int value);
        public int GetInt(string name);
    }
}