using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;

    public GameObject lostPanel;

    public static GameManager instance;

    public Slider sliderTapBonus;

    public List<GameObject> levelList = new List<GameObject>();

    public int levelCurrent;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
            
    //    }
    //}
    public void Win()
    {
        winPanel.SetActive(true);  
    }
    public void Lost()
    {
        lostPanel.SetActive(true);
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
    public void NextLevel()
    {

    }
    

}
