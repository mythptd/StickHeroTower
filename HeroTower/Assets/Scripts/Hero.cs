using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;

public class Hero : MonoBehaviour
{
    
    public MonsterType monsterType;

    public int powerId;

    public TextMeshPro textPower;

    private Vector3 pos;

    private Animator anim;

    public List<GameObject> heroList = new List<GameObject> ();

    public bool move;

    

    void Start()
    {
        
        anim = gameObject.GetComponentInChildren<Animator>();
        textPower = transform.GetChild(0).GetComponent<TextMeshPro>();
        SetText();
    }
    private void Update()
    {
        if (!move)
        {
            return;
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards (transform.localPosition,new Vector3(0.5f,0,-2), 6 * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, new Vector2(0.5f, 0)) < 0.001f)
            {
                move = false;
            }
        }

    }

    public void AnimAttack()
    {
        anim.SetTrigger("Attack");
    }
    public void SetText()
    {
        textPower.text = powerId.ToString();
    }
    public void ChangePlayer(int id)
    {
        for (int i = 0; i < heroList.Count; i++)
        {
            heroList[i].SetActive(false);
            heroList[id].SetActive(true);
            anim = gameObject.GetComponentInChildren<Animator>();
        }
    }
    public IEnumerator Box()
    {
        yield return new WaitForSeconds(1);
        move = true;
    }
    public void OpenBox()
    {
        if (EnemyManager.instance.enemyList.Count <= 0)
        {
            StartCoroutine(Box());
        }

    }
    public void MoveToBoss(GameObject boss)
    {
        Vector3 pos = boss.transform.position - new Vector3(2, 4, 0);
        transform.DOMove(pos,1).SetDelay(1);
    }
}
