using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    public int powerId;
    private TextMeshPro textPower;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        textPower = transform.GetChild(0).GetComponent<TextMeshPro>();

        textPower.text = powerId.ToString();
    }
    public void AnimAttack()
    {
        anim.SetTrigger("Attack");
        
    }
    public void BossMove()
    {
        transform.DOMoveX(transform.position.x + 4,1);
    }
}
