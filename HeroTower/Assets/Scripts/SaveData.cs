
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData
{
    private string savePath;
    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath,"SaveFile");
    }
    public void Save()
    {
        using (var binaryWriter = new BinaryWriter(File.Open(savePath, FileMode.Create)))
        {
            binaryWriter.Write(GameManager.instance.levelCurrent);
            Debug.Log(savePath);
        }
           
    }
    public void Load()
    {
        using (var binaryReader = new BinaryWriter(File.Open(savePath, FileMode.Open)))
        {
           
        }
    }
}
