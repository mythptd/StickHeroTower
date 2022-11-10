using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Box : MonoBehaviour
{
    //public List<GameObject> listBoxEnemy = new List<GameObject> ();
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        if (base.tag == "BoxEnemy")
        {
            EnemyManager.instance.box.Add(gameObject);
        }
        GetEnemy();
        //foreach (Transform item in transform)
        //{
        //    if (item.tag == "Enemy")
        //    {
        //        listBoxEnemy.Add(item.gameObject);
        //    }
        //}
    }
    public void GetEnemy()
    {
        if (transform.childCount > 0)
        {
            enemy = transform.GetChild(0).gameObject;
        }
    }
}
