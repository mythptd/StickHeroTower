using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Sprite lockSprite;

    private Image image;

    public TextMeshProUGUI textMeshProUGUI;

    private int level = 0;

    private Button button;

    private void OnEnable()
    {
        image = gameObject.GetComponent<Image>();
        button = gameObject.GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(OnClickSpawnMap);


    }
    public void SetUp(int level,bool unlock)
    {
        this.level = level;
        textMeshProUGUI.text = level.ToString();

        if (unlock)
        {
            image.sprite = null;
            button.enabled = true;
            textMeshProUGUI.gameObject.SetActive(true);

        }
        else
        {
            image.sprite = lockSprite;            
            button.enabled = false;
            textMeshProUGUI.gameObject.SetActive(false);
        }
    }
    public void OnClickSpawnMap()
    {
        Debug.Log("SpawnMap");
        GameManager.instance.SpawnMap(level-1);
        GameManager.instance.inStage = true;
    }
}
