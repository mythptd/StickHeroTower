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

    private bool drag;
 
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (drag) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (GameManager.instance.sliderTapBonus.isActiveAndEnabled)
            {
               GameManager.instance.sliderTapBonus.value += 0.15f;

            }
            else if (targetObject && targetObject.tag == "Player")
            {
                //liftedfrom = targetObject.transform.position;
                selectedObject = targetObject.transform.gameObject;
                liftedfrom = selectedObject.transform.position;
                offset = selectedObject.transform.position - mousePosition;
                hero = selectedObject.GetComponent<Hero>();
                hero.Drag();
                selectedObject.transform.parent = null;

            }
        }
        if (Input.GetMouseButton(0))
        {
            if (selectedObject)
            {
                //hero.AnimOverHead();
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
                                StartCoroutine(hero.Drop());

                                hero.GetComponent<Hero>().StartEffect("effect");
                                selectedObject.transform.SetParent(hit.collider.transform);
                                selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.4f, -0.3f, -2);
                                hero.powerId += getType.GetComponent<Weapon>().powerId;
                                hero.SetText();
                                Destroy(getType);
                                hero.monsterType = getType.GetComponent<Weapon>().monsterType;
                                hero.ChangePlayer();

                            }
                            else if (getType.GetComponent<Trap>() != null)
                            {
                                StartCoroutine(hero.Drop());

                                selectedObject.transform.SetParent(hit.collider.transform);
                                selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.4f, -0.3f, -2);
                                hero.powerId -= getType.GetComponent<Trap>().powerId;
                                hero.SetText();
                                if (hero.powerId <= 0)
                                {
                                    hero.AnimHeroDie();
                                    GameManager.instance.Lost();
                                }
                                getType.GetComponent<Trap>().Trapping();
                                //Destroy(getType);
                            }
                            else if (getType.GetComponent<Armor>() != null)
                            {
                                StartCoroutine(hero.Drop());
                                hero.GetComponent<Hero>().StartEffect("effect_giap");
                                selectedObject.transform.SetParent(hit.collider.transform);
                                selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.4f, -0.3f, -2);
                                hero.powerId += getType.GetComponent<Armor>().powerId;
                                hero.SetText();                               
                                Destroy(getType);
                            }
                            else if (getType.tag == "Enemy" && getType != null && hero.monsterType == getType.GetComponent<Enemy>().monsterType)
                            {
                                
                                selectedObject.transform.SetParent(hit.collider.transform);
                                selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.4f, -0.3f, -2);
                                if (hero.powerId >= getType.GetComponent<Enemy>().powerIdEnemy)
                                {             
                                    
                                    StartCoroutine(hero.Drop());
                                    hero.AnimAttack();
                                    StartCoroutine(DelayDrag());
                                    hero.powerId += getType.GetComponent<Enemy>().powerIdEnemy;
                                    hero.SetText();
                                    getType.GetComponent<Enemy>().AnimEnemyDie();
                                    EnemyManager.instance.enemyList.Remove(getType);
                                                                      
                                    //selectedObject.GetComponent<Hero>().OpenBox();
                                }
                                else
                                {
                                    getType.GetComponent<Enemy>().AnimAttack();
                                    hero.AnimHeroDie();

                                    GameManager.instance.Lost();

                                }
                            }
                            else
                            {
                                SendBackToTile();
                                StartCoroutine(hero.Drop());

                            }
                            StartCoroutine(EnemyManager.instance.CheckWin(selectedObject));
                        }
                        else
                        {
                            StartCoroutine(hero.Drop());

                        }
                    }
                    else
                    {
                        StartCoroutine(hero.Drop());

                        selectedObject.transform.SetParent(hit.collider.transform);
                        selectedObject.transform.position = hit.collider.transform.position + new Vector3(-0.4f, -0.3f, -2);
                        hit.collider.GetComponent<Box>().GetEnemy();
                    }
                    EnemyManager.instance.Colum();
                    EnemyManager.instance.DestroyBox();
                    selectedObject.transform.SetParent(hit.collider.transform);

                }
                else
                {
                    StartCoroutine(hero.Drop());

                    SendBackToTile();
                }
                selectedObject = null;

            }
        }     
    }
   
    public void SendBackToTile()
    {
        selectedObject.transform.position = liftedfrom;
    }
    public IEnumerator DelayDrag()
    {
        drag = true;
        yield return new WaitForSeconds(1f);
        drag = false;
    }
    //public void CompareStrength(GameObject player,GameObject Enemy)
    //{

    //}
  
}
