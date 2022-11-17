using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraFollow : MonoBehaviour
{
    private int posX;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        transform.DOMove(Vector3.zero, 3).OnComplete(() =>
        {
            EnemyManager.instance.AddColum();
            EnemyManager.instance.GetColumNext();

        }); 

    }
    private void Update()
    {
        //cam.orthographicSize += 1 * Time.deltaTime;
        //if (cam.orthographicSize > 9)
        //{
        //    cam.orthographicSize = 9;
        //}
    }

    public void MoveCam()
    {
        posX += 3;        
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
}
