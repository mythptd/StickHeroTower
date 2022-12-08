using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    private float posX;
    private Vector2 originalLocation;
    //private Camera cam;
    /// Start is called before the first frame update
    void Start()
    {
        originalLocation = transform.position;
        //cam = GetComponent<Camera>();
        //transform.position = new Vector3(transform.position.x+3,0,0);
        //StartGame();
    }

    public void MoveCam()
    {
        posX += 3.2f;        
        transform.DOMoveX(posX, 0.5f).OnComplete(() =>
        {
            EnemyManager.instance.AddColum();
            EnemyManager.instance.GetColumNext();

        });
    }
    public void MoveToBoss(Transform boss)
    {
        transform.DOMoveX(boss.position.x, 1f);

    }
    public void StartGame()
    {
        transform.DOMove(Vector3.zero, 2).OnComplete(() =>
        {
            EnemyManager.instance.AddColum();
            EnemyManager.instance.GetColumNext();

        });
    }
    public void ResetCam()
    {
        transform.position = originalLocation;
    }
}
