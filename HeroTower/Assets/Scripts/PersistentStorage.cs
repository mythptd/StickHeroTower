using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistentStorage : MonoBehaviour
{
    string path;
    public GameManager gameManager;
    //public int level;
    private void Awake()
    {
        path = Path.Combine(Application.persistentDataPath,"SaveFile");

    }
    private void Start()
    {
        
    }
    public void Save()
    {
        using (var writer = new BinaryWriter(File.Open(path,FileMode.Create)))
        {
            writer.Write(gameManager.levelCurrent);

            writer.Write(gameManager.levelNumber);

            writer.Write(gameManager.gold);

            writer.Write(gameManager.unlockedLevel);

            writer.Write(gameManager.sound);

            writer.Write(gameManager.textLevel.Count);
            for (int i = 0; i < gameManager.textLevel.Count; i++)
            {               
                writer.Write(gameManager.textLevel[i].text);
            }
            Debug.Log(path);

        }
    }
    public void Load()
    {
        using (var read = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            gameManager.levelCurrent = read.ReadInt32();

            gameManager.levelNumber = read.ReadInt32();

            gameManager.gold = read.ReadInt32();

            gameManager.unlockedLevel = read.ReadInt32();

            gameManager.sound = read.ReadBoolean();
            
            int count = read.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string a = read.ReadString();
                gameManager.textLevel[i].text = a;
            }
        }
    }
}
