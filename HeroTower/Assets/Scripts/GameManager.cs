using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static Cinemachine.DocumentationSortingAttribute;
using Spine.Unity;

public class GameManager : MonoBehaviour
{
    [Header("GAMEPLAY")]
    public GameObject winPanel;
    public GameObject lostPanel;
    public Slider sliderTapBonus;
    [Header("MAPTEXT")]
    public List<GameObject> levelImage = new List<GameObject>();
    public int levelNumber;
    public List<TextMeshProUGUI> textLevel = new List<TextMeshProUGUI>();
    public int saveTextlevel;

    [Header("MAPLIST")]
    public List<GameObject> levelList = new List<GameObject>();
    private GameObject map;
    public int levelCurrent;


    
    [Space]
    [Header("MENU")]
    public GameObject menuPanel;
    public GameObject gamePlayPanel;
    public GameObject settingPanel;

    [Header("STAGE")]
    public GameObject stagePanel;
    public int unlockedLevel;
    public bool inStage;
    private int levelMap;

    //public Hero player;

    [Space]
    [Header("COINGAME")]
    public int gold;
    public TextMeshProUGUI goldText;    
    public int diamond;
    public TextMeshProUGUI diamondText;

    [Space]
    [Header("SHOP")]
    public GameObject shop;
    public GameObject scrollViewCharacter;
    public GameObject scrollViewTheme;
    public SkeletonAnimation playerShop;

   


    [Space]
    public PersistentStorage storage;
    public CameraFollow cam;
    public static GameManager instance;
    [Space]
    public bool sound;
    public Toggle toggleSound;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AudioManager.instance.Play("MusicMenu");
        storage.Load();
        goldText.text = gold.ToString();
        diamondText.text = diamond.ToString();

        toggleSound.onValueChanged.AddListener(Sound);
        toggleSound.isOn = sound;

        //Debug.Log("level đã lưu" + levelCurrent);
    }
    //public void SaveData(GameDataWriter gameDataWriter)
    //{
    //    gameDataWriter.Write(levelCurrent);
    //}
    //public void LoadData(GameDataReader gameDataReader)
    //{
    //    levelCurrent = gameDataReader.ReadInt();
    //}
    public void Win()
    {
        winPanel.SetActive(true);  
        winPanel.transform.DOScale(Vector3.one, 1).SetDelay(2).From(0);

    }
    public void Lost()
    {
        lostPanel.SetActive(true);
        lostPanel.transform.DOScale(Vector3.one,1).SetDelay(2).From(0);
    }
    public IEnumerator TapBonus()
    {
        sliderTapBonus.gameObject.SetActive(true);
        sliderTapBonus.value = 1;
        while (true)
        {
            sliderTapBonus.value -= 0.005f;
            yield return null;
        }
    }
    public void CheckTapBonus()
    {
        float t = Mathf.Clamp01((float)sliderTapBonus.value);
        if (t <= 0.5f)
        {
            gold += 200;            
        }
        else if( 0.5f < t && t <1)
        {
            gold += 300;
        }
    }
    public void NextLevel()
    {
        //gold += 100;
        //CheckTapBonus();

        goldText.text = gold.ToString();

        EnemyManager.instance.box.Clear();
        EnemyManager.instance.colum.Clear();

        sliderTapBonus.gameObject.SetActive(false);
        cam.ResetCam();
        cam.StartGame();
        winPanel.SetActive(false);
        Destroy(map);

        if (inStage)
        {
            AudioManager.instance.Pause("MusicGamePlay");
            AudioManager.instance.Play("MusicMenu");
            gamePlayPanel.SetActive(false);
            menuPanel.SetActive(true);
            stagePanel.SetActive(false);
            GameManager.instance.inStage = false;
        }
        else
        {
            levelCurrent += 1;
            if (levelCurrent > levelList.Count - 1)
            {
                levelCurrent = levelList.Count-1;
            }
            map = Instantiate(levelList[levelCurrent]);

        }


        EnemyManager.instance.FindBoss();


        for (int i = 0; i < levelImage.Count; i++)
        {
            levelImage[i].GetComponent<Image>().color = Color.white;
        }

        if (levelNumber < levelImage.Count - 1)
        {
            levelNumber += 1;
            levelImage[levelNumber].GetComponent<Image>().color = Color.blue;
        }
        else
        {
            for (int i = 0; i < textLevel.Count; i++)
            {
                saveTextlevel = int.Parse(textLevel[i].text);
                saveTextlevel += 5;
                textLevel[i].text = saveTextlevel.ToString();
            }
            levelNumber = 0;
            levelImage[levelNumber].GetComponent<Image>().color = Color.blue;

        }
       
        //Debug.Log(levelNumber);

    }
    public void PlayAgain()
    {
        EnemyManager.instance.box.Clear();
        EnemyManager.instance.colum.Clear();

        sliderTapBonus.gameObject.SetActive(false);
        cam.ResetCam();
        cam.StartGame();
        lostPanel.SetActive(false);
      
        Destroy(map);
        if (inStage)
        {
            map = Instantiate(levelList[levelMap]);
        }
        else
        {
            map = Instantiate(levelList[levelCurrent]);

        }

        EnemyManager.instance.FindBoss();
    }
    
    private void OnApplicationQuit()
    {
        storage.Save();
        //Debug.Log(levelCurrent);
    }
    public void StartGame()
    {
        AudioManager.instance.Play("MusicGamePlay");
        AudioManager.instance.Pause("MusicMenu");


        for (int i = 0; i < levelImage.Count; i++)
        {
            levelImage[i].GetComponent<Image>().color = Color.white;
        }

        //storage.Load();
        levelImage[levelNumber].GetComponent<Image>().color = Color.blue;

        gamePlayPanel.SetActive(true);
        menuPanel.SetActive(false);
        cam.StartGame();

        map = Instantiate(levelList[levelCurrent]);


        EnemyManager.instance.FindBoss();
        levelImage[levelNumber].GetComponent<Image>().color = Color.blue;
    }
    public void Setting()
    {
        settingPanel.SetActive(true);
    }
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }
    public void ButtonHome()
    {
        inStage = false;
        AudioManager.instance.Pause("MusicGamePlay");
        AudioManager.instance.Play("MusicMenu");
        gamePlayPanel.SetActive(false);
        menuPanel.SetActive(true);
        stagePanel.SetActive(false);
        Destroy(map);
        cam.ResetCam();
        storage.Save();
    }
    public void RestartGame()
    {
        EnemyManager.instance.box.Clear();
        EnemyManager.instance.colum.Clear();

        sliderTapBonus.gameObject.SetActive(false);
        cam.StartGame();
        lostPanel.SetActive(false);

        Destroy(map);
        if (inStage)
        {
            map = Instantiate(levelList[levelMap]);
            
        }
        else
        {
            map = Instantiate(levelList[levelCurrent]);

        }

        EnemyManager.instance.FindBoss();
        cam.ResetCam();
    }
    public void BtnStage()
    {
        stagePanel.SetActive(true);
    }
    public void CloseStage()
    {
        stagePanel.SetActive(false);
    }
    public void SpawnMap(int levelMap)
    {
        this.levelMap = levelMap;
        map = Instantiate(levelList[levelMap]);
        gamePlayPanel.SetActive(true);
        menuPanel.SetActive(false);
        cam.StartGame();
        EnemyManager.instance.FindBoss();
        for (int i = 0; i < levelImage.Count; i++)
        {
            levelImage[i].GetComponent<Image>().color = Color.white;
        }

        levelImage[levelMap].GetComponent<Image>().color = Color.blue;
    }
    public void Sound(bool soundToggle)
    {
        this.sound = soundToggle;
        //soundToggle = !soundToggle;
        //soundToggle = toggle.isOn;
        if (sound)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
        
    }
    public void ShowShop()
    {
        menuPanel.SetActive(false);
        shop.SetActive(true);
    }
    public void CloseShop()
    {
        menuPanel.SetActive(true);
        shop.SetActive(false);
    }
    public void BtnCharacter()
    {
        scrollViewCharacter.SetActive(true);
        scrollViewTheme.SetActive(false);

    }
    public void BtnTheme()
    {
        scrollViewCharacter.SetActive(false);
        scrollViewTheme.SetActive(true);
    }
    public void SetSkin(string skinName)
    {
        playerShop.Skeleton.SetSkin(skinName);
    }
}
