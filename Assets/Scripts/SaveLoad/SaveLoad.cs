﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad{
	string defaultPath = Application.persistentDataPath;

	public void SaveData<T>(T data, string fileName = null, string path = null) where T: class
	{
		if(path == null) path = defaultPath;

		if (!Directory.Exists (path))
                Directory.CreateDirectory (path);            

		if(fileName == null)
            path = Path.Combine(path, typeof(T)+".dat"); 
        else
            path = Path.Combine(path, fileName+".dat"); 

		BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize (fileStream, data);
        }
	}

	public T LoadData<T> (string fileName = null, string path = null) where T: class
    {	
		if(path == null) path = defaultPath;
        
		if(fileName == null)
            path = Path.Combine(path, typeof(T)+".dat"); 
        else
            path = Path.Combine(path, fileName+".dat"); 
        
		if (!File.Exists (path))
            return null;
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.Open))
        {
            return (T)binaryFormatter.Deserialize (fileStream);
        }
    }
}
