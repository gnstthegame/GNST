using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class saveSystem{

    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        savedata data = new savedata(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static savedata Loaddata (Player player)
    {
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            savedata data = formatter.Deserialize(stream) as savedata;
            stream.Close();

            return data;
             
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
