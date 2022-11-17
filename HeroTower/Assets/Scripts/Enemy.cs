using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public MonsterType monsterType;
    
    public int powerIdEnemy;
    public TextMeshPro textPower;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.instance.enemyList.Add(gameObject);

        anim = gameObject.GetComponentInChildren<Animator>();

        textPower = transform.GetChild(0).GetComponent<TextMeshPro>();

        textPower.text = powerIdEnemy.ToString();

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward,20,layerMask);
        //if (hit.collider != null)
        //{
        //    hit.collider.GetComponent<Box>().enemy = gameObject;
        //}

    }
    public void AnimAttack()
    {
        anim.SetTrigger("Attack");

    }

}
