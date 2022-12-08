
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameDataWriter
{
    BinaryWriter binaryWriter;
    private string savePath;
   public GameDataWriter(BinaryWriter binaryWriter)
    {
        this.binaryWriter = binaryWriter;   
    }
    public void Write(int value)
    {
        binaryWriter.Write(value);
    }
    public void Write(float value)
    {
        binaryWriter.Write(value);
    }
}
