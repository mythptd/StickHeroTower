using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> box;

    public List<GameObject> enemyList;

    public static EnemyManager instance;

    public GameObject game;

    public RaycastHit2D[] hit;

    public LayerMask layerMask;

    public List<GameObject> colum;

    private CameraFollow cameraFollow;

    //private int numberBox;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();

        AddColum(); 
        Debug.Log(colum.Count);
        
    }
    public void DestroyBox()
    {

        foreach (var i in box)
        {
            if (i.transform.gameObject.tag == "BoxEnemy" && i.transform.childCount <= 0)
            {
                if (colum.Count - 1 <= 1)
                {
                    return;
                }
                Destroy(i.transform.gameObject);
                box.Remove(i.transform.gameObject);
                return;
            }
        }
    }
    public void Colum()
    {
        int i = 0 ;
        do
        {
            foreach (GameObject item in colum)
            {
                if (item.transform.childCount > 0)
                {
                    cameraFollow.MoveCam();
                    colum.Clear();
                    AddColum();
                    return;
                }
            }
        } while (i < colum.Count);
        //foreach (GameObject item in colum)
        //{
           
        //    if (item.transform.childCount <= 0)
        //    {
        //        cameraFollow.MoveCam();
        //        colum.Clear();
        //        AddColum();
        //        return;
        //    }
        //}
    }
    public void AddColum()
    {
        hit = Physics2D.RaycastAll(game.transform.position, Vector2.up);
        foreach (var item in hit)
        {
            colum.Add(item.transform.gameObject);

            //if (item.transform.tag == "BoxEnemy")
            //{
            //}
        }
    }
}
