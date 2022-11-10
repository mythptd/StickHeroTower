using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int powerId;
    public TextMeshPro textPower;
    // Start is called before the first frame update
    void Start()
    {
        textPower = transform.GetChild(0).GetComponent<TextMeshPro>();

        textPower.text = "-" + powerId.ToString();
    }

  
}
