using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> box;

    public List<GameObject> enemyList;

    public static EnemyManager instance;

    public GameObject posRay;

    public GameObject NextColum;

    public bool moveCam;

    public RaycastHit2D[] hitColumCurrent;

    public RaycastHit2D[] hitColumNext;

    public LayerMask layerMask;

    public List<GameObject> colum;

    public CameraFollow cameraFollow;

    private GameObject chest;

    private GameObject boss;


    //private int numberBox;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()

    {
        chest = GameObject.FindGameObjectWithTag("Chest");
        boss = GameObject.FindGameObjectWithTag("Boss");

        //cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();

        //AddColum(); 
        Debug.Log(colum.Count);
        
    }
    public void DestroyBox()
    {
        for (int i = 0; i < colum.Count -2; i++)
        {
            if (colum[i].transform.childCount <= 0)
            {
                Destroy(colum[i]);
                colum.RemoveAt(i);
                return;
            }

        }
    }

    public void Colum()
    {
        for (int i = 0; i < colum.Count; i++)
        {
            if (colum[i].transform.childCount <= 0)
            {
                if (colum.Count - i == 1 && moveCam)
                {
                    cameraFollow.MoveCam();
                    colum.Clear();

                }
            }
            else 
            {
                break;
            }           
        }
        //int a = 0;
        //foreach (GameObject item in colum)
        //{          
        //    if (item.transform.childCount > 0)
        //    {
        //        break;
        //    }
        //    else
        //    {
        //        a += 1;
        //        if (colum.Count - a == 0)
        //        {
        //            cameraFollow.MoveCam();
        //        }               
        //    }
        //}
    }
    public void AddColum()
    {
        hitColumCurrent = Physics2D.RaycastAll(posRay.transform.position, Vector2.up);
        foreach (var item in hitColumCurrent)
        {

            if (item.transform.tag == "Box" || item.transform.tag == "BoxEnemy")
            {
                colum.Add(item.transform.gameObject);

            }
        }
    }
    public void GetColumNext()
    {
        hitColumNext = Physics2D.RaycastAll(NextColum.transform.position, Vector2.up);
        foreach (var item in hitColumNext)
        {

            if (item.transform.tag == "BoxEnemy")
            {
                moveCam = true;
            }
            else
            {
                moveCam = false;
            }
        }
    }
    public IEnumerator CheckWin( GameObject selectedObject)
    {
        if (enemyList.Count <= 0)
        {
            if (chest != null)
            {
                selectedObject.GetComponent<Hero>().OpenBox();
                chest.GetComponent<Animator>().Play("Chest");
                yield return new WaitForSeconds(2);
                GameManager.instance.Win();

            }
            else if (boss != null)
            {
                cameraFollow.MoveToBoss(boss.transform);
                selectedObject.GetComponent<Hero>().MoveToBoss(boss);
                yield return new WaitForSeconds(1);
                StartCoroutine(GameManager.instance.TapBonus());

                if (selectedObject.GetComponent<Hero>().powerId >= boss.GetComponent<Boss>().powerId)
                {                    
                    yield return new WaitForSeconds(2);
                    selectedObject.GetComponent<Hero>().AnimAttack();
                    Destroy(boss);
                    yield return new WaitForSeconds(2);
                    GameManager.instance.Win();
                }
                else
                {
                    yield return new WaitForSeconds(2);
                    boss.GetComponent<Boss>().AnimAttack();
                    Destroy(selectedObject);
                    yield return new WaitForSeconds(2);
                    GameManager.instance.Lost();

                }
            }
            else
            {
                yield return new WaitForSeconds(2);
                GameManager.instance.Win();
            }
        }
    }
  

}
