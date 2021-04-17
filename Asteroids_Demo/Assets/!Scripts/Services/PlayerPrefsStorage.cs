using Services;
using UnityEngine;
public class PlayerPrefsStorage : IDataStorage
{
    public int GetInt(string name)
    {
        return PlayerPrefs.GetInt(name);
    }

    public void SaveInt(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
}
