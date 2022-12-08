using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectCharacter : MonoBehaviour
{
    private Button select;
   
    void Start()
    {
        select = gameObject.GetComponent<Button>();
        //select.onClick.AddListener(OnClick);
    }
    public void OnClick(string skinName)
    {
        GameManager.instance.SetSkin(skinName);
    }
}
