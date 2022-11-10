using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public MonsterType monsterType;
    public int powerId;
    public TextMeshPro textPower;
    public int idWeapon;
    // Start is called before the first frame update
    void Start()
    {
        textPower = transform.GetChild(0).GetComponent<TextMeshPro>();

        textPower.text = powerId.ToString();
    }

   
}
