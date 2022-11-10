using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    
    public LayerMask LayerMask;

    public GameObject selectedObject;

    private Vector3 offset;

    private Vector3 liftedfrom;

    public GameObject player;

    private RaycastHit2D hit;

    private Hero hero;

    private GameObject chest;
    // Start is called before the first frame update
    void Start()
    {
        chest = GameObject.FindGameObjectWithTag("Chest");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.tag == "Player")
            {
                //liftedfrom = targetObject.transform.position;
                selectedObject = targetObject.transform.gameObject;
                liftedfrom = selectedObject.transform.position;
                offset = selectedObject.transform.position - mousePosition;
                hero = selectedObject.GetComponent<Hero>();
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (selectedObject)
            {
                selectedObject.transform.position = mousePosition + offset;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
           
            if (selectedObject)
            {
               // Debug.DrawRay(selectedObject.transform.position, Vector3.forward * 100, Color.yellow);

                hit = Physics2D.Raycast(selectedObject.transform.position, Vector3.forward, 20, LayerMask);
                //Enemy enemy = hit.collider.GetComponent<Box>().e;

                if (hit.collider != null)
                {
                    if ( hit.collider.transform.childCount > 0)
                    {
                        GameObject getType = hit.collider.transform.GetChild(0).gameObject;
                        if (getType != null)
                        {
                            if (getType.GetComponent<Weapon>() != null)
                            {
                                
                                selectedObject.transform.SetParent(hit.collider.transform);
                                selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.5f, 0, -2);
                                hero.powerId += getType.GetComponent<Weapon>().powerId;
                                hero.SetText();
                                Destroy(getType);
                                hero.ChangePlayer(getType.GetComponent<Weapon>().idWeapon);
                            }
                            else if (getType.GetComponent<Trap>() != null)
                            {
                                selectedObject.transform.SetParent(hit.collider.transform);
                                selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.5f, 0, -2);
                                hero.powerId -= getType.GetComponent<Trap>().powerId;
                                hero.SetText();
                                if (hero.powerId <= 0)
                                {
                                    Destroy(selectedObject);
                                }
                                Destroy(getType);
                            }
                            else if (getType.GetComponent<Armor>() != null)
                            {
                                selectedObject.transform.SetParent(hit.collider.transform);
                                selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.5f, 0, -2);
                                hero.powerId += getType.GetComponent<Armor>().powerId;
                                hero.SetText();                               
                                Destroy(getType);
                            }
                            else if (getType.tag == "Enemy" && getType != null && hero.monsterType == getType.GetComponent<Enemy>().monsterType)
                            {
                                
                                selectedObject.transform.SetParent(hit.collider.transform);
                                selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.5f, 0, -2);
                                if (hero.powerId >= getType.GetComponent<Enemy>().powerIdEnemy)
                                {
                                    hero.AnimAttack();
                                    hero.powerId += getType.GetComponent<Enemy>().powerIdEnemy;
                                    hero.SetText();
                                    Destroy(getType);
                                    EnemyManager.instance.enemyList.Remove(getType);
                                    if (EnemyManager.instance.enemyList.Count <= 0)
                                    {                                       
                                        if (chest != null)
                                        {                                            
                                            selectedObject.GetComponent<Hero>().OpenBox();
                                            chest.GetComponent<Animator>().Play("Chest");
                                        }
                                    }
                                }
                                else
                                {
                                    getType.GetComponent<Enemy>().AnimAttack();
                                    Destroy(selectedObject);
                                }
                            }
                            else
                            {
                                SendBackToTile();
                            }
                            EnemyManager.instance.Colum();

                        }
                    }
                    else
                    {
                        selectedObject.transform.SetParent(hit.collider.transform);
                        selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.5f, 0, -2);
                    }
                }
                else
                {
                    //Debug.Log("nothing");
                    SendBackToTile();
                }
                selectedObject = null;
                //EnemyManager.instance.DestroyBox();
            }
        }
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Ended)
        //    {
               

        //    }
        //}
      
    }
   
    public void SendBackToTile()
    {
        selectedObject.transform.position = liftedfrom;
    }
  
}
