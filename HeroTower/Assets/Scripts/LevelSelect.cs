using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public int totalLevel;

    private int page =0;

    private int pageItem = 10;

    //public int unlockedLevel;

    private int totalPage =0;

    private LevelButton[] levelButtons;

    public GameObject nextBtn;

    public GameObject backBtn;

    public TextMeshProUGUI textChooseLevel;
    private int firstNumber = 1;
    private int lastNumber = 10;

    private void OnEnable()
    {
          levelButtons = GetComponentsInChildren<LevelButton>();
    }
    private void Start()
    {
        textChooseLevel.text = "Level " + firstNumber + "-" + lastNumber;
        Refresh();    
    }

    public void NextPage()
    {

        page += 1;
        Refresh();
        firstNumber += 10;
        lastNumber += 10;
        textChooseLevel.text = "Level " + firstNumber + "-" + lastNumber;

    }
    public void BackPage()
    {
        page -= 1;
        Refresh();
        firstNumber -= 10;
        lastNumber -= 10;
        textChooseLevel.text = "Level " + firstNumber + "-" + lastNumber;
    }
    public void Refresh()
    {
        totalPage = totalLevel / pageItem;
        int index = page * pageItem;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = index + i + 1;
            if (level <= totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].SetUp(level, level <= GameManager.instance.unlockedLevel);
            }
            else
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }
        CheckBtn();
    }
    public void CheckBtn()
    {
        backBtn.SetActive(page > 0);
        nextBtn.SetActive(page < totalPage);
    }
}
