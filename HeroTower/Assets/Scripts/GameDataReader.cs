using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataReader
{
    BinaryReader binaryReader;
    public GameDataReader(BinaryReader binaryReader)
    {
        this.binaryReader = binaryReader;
    }
    public int ReadInt()
    {
        return binaryReader.ReadInt32();
    }
    public float ReadFloat()
    {
       return binaryReader.ReadSingle();
    }
}
