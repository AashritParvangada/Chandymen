using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame
{
    public static void SavePlayer(Scene_Manager _ScnMan, GameManager _GmMan)
    {
        BinaryFormatter _formatter = new BinaryFormatter();
        string _path = Application.persistentDataPath + "/gameProgress.chandy";
        FileStream _stream = new FileStream(_path, FileMode.Create);

        PlayerData _data = new PlayerData(_ScnMan, _GmMan);

        _formatter.Serialize(_stream, _data);
        _stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string _path = Application.persistentDataPath + "/gameProgress.chandy";

        if (File.Exists(_path))
        {
            BinaryFormatter _formatter = new BinaryFormatter();
            FileStream _stream = new FileStream(_path, FileMode.Open);

            PlayerData _data = _formatter.Deserialize(_stream) as PlayerData;
            _stream.Close();
            return _data;
        }

        else
        {
            Debug.LogError("Save file not found in " + _path);
            return null;
        }
    }
}
