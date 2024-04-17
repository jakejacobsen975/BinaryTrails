using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.Common;
using UnityEngine.Analytics;

public static class SaveData {
   
    public static void SavePlayerData (TopDownController player){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/BinaryTrails.save";
        FileStream stream = new(path, FileMode.Create);
        
        PlayerData data = new(player);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData loadPlayer (){
         string path = Application.persistentDataPath + "/BinaryTrails.save";
         if(File.Exists(path)){
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            
            return data;
         }else{
            Debug.LogWarning("Save file not found in"+ path);
            return null;
         }
    }
    public static void SavePortalName (string portalName){
        PlayerData data = loadPlayer();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/BinaryTrails.save";
        FileStream stream = new(path, FileMode.Create);
        
        data.completedLevels.Add(portalName);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void resetPosition (){
        PlayerData data = loadPlayer();
        if (data != null) {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/BinaryTrails.save";
            FileStream stream = new(path, FileMode.Create);
            
            data.position = new float[] { 0, 0 };
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }
    

}
