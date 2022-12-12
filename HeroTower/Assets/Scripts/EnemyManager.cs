using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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
        //FindBoss();

        //cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();

        //AddColum(); 
        //Debug.Log(colum.Count);
        
    }
    public void DestroyBox()
    {
        for (int i = 0; i <= colum.Count -2; i++)
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
    }
    public void AddColum()
    {
        hitColumCurrent = Physics2D.RaycastAll(posRay.transform.position, Vector2.up);
        for (int i = 0; i < hitColumCurrent.Length; i++)
        {
            if (hitColumCurrent[i].transform.tag == "Box" || hitColumCurrent[i].transform.tag == "BoxEnemy")
            {
                colum.Add(hitColumCurrent[i].transform.gameObject);
                
            }
        }
        //foreach (var item in hitColumCurrent)
        //{

            
        //}
    }
    public void GetColumNext()
    {
        hitColumNext = Physics2D.RaycastAll(NextColum.transform.position, Vector2.up);
        foreach (var item in hitColumNext)
        {

            if (item.transform.tag == "BoxEnemy")
            {
                moveCam = true;
                return;
                //Debug.Log("true");
            }
            else
            {
                moveCam = false;
                
                //Debug.Log("false");

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
                yield return new WaitForSeconds(1);
                chest.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "box_open", true);
                yield return new WaitForSeconds(1.5f);
                //selectedObject.transform.parent = null;
                GameManager.instance.Win();
                selectedObject.GetComponent<Hero>().AnimWin();

            }
            else if (boss != null)
            {
                yield return new WaitForSeconds(1);
                cameraFollow.MoveToBoss(boss.transform);
                selectedObject.GetComponent<Hero>().MoveToBoss(boss);
                yield return new WaitForSeconds(1);

                if (selectedObject.GetComponent<Hero>().powerId >= boss.GetComponent<Boss>().powerId)
                {
                    StartCoroutine(GameManager.instance.TapBonus());
                    yield return new WaitForSeconds(2);
                    selectedObject.GetComponent<Hero>().AnimAttack();
                    yield return new WaitForSeconds(1);
                    boss.GetComponent<Boss>().AnimEnemyDie();
                    selectedObject.GetComponent<Hero>().AnimWin();
                    //Destroy(boss);
                    StopAllCoroutines();
                    GameManager.instance.CheckTapBonus();                    
                    Vibration.Vibrate(1);
                    GameManager.instance.Win();

                }
                else
                {
                    yield return new WaitForSeconds(2);
                    boss.GetComponent<Boss>().AnimAttack();
                    selectedObject.GetComponent<Hero>().AnimHeroDie();
                    //Destroy(selectedObject);                    
                    Vibration.Vibrate(1);
                    GameManager.instance.Lost();
                }
            }
            else
            {
                selectedObject.GetComponent<Hero>().AnimWin();
                yield return new WaitForSeconds(1.5f);
                //selectedObject.transform.parent = null;
                GameManager.instance.Win();

            }
        }
    }
    public void FindBoss()
    {
        chest = GameObject.FindGameObjectWithTag("Chest");
        boss = GameObject.FindGameObjectWithTag("Boss");
    }


}
