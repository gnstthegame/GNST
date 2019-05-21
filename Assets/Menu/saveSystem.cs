using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// klasa zapisuje i wczytuje stan gry z pliku
/// </summary>
public class saveSystem{
    /// <summary>
    /// zapis
    /// </summary>
    /// <param name="player">SaveLinker</param>
    public static void SavePlayer(SaveLinker player) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        savedata data = new savedata(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    /// <summary>
    /// usuń zapis
    /// </summary>
    public static void DeletePlayer() {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path)) {
            File.Delete(path);
        }
        Debug.Log("save deleted");
    }
    /// <summary>
    /// wczytaj zapis
    /// </summary>
    /// <param name="player">SaveLinker</param>
    /// <returns>savedata</returns>
    public static savedata Loaddata (SaveLinker player)
    {
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length<1) {
                stream.Close();
                Debug.Log("Save is empty " + path);
                return null;
            }
            savedata data = formatter.Deserialize(stream) as savedata;
            stream.Close();

            return data;
             
        } else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
