using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;
using Spine.Unity;
using Spine;

public class Hero : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public SkeletonAnimation effect;
    public Spine.AnimationState animationState;
    //public Spine.Skeleton skeleton;

    public MonsterType monsterType;

    public int powerId;

    public TextMeshPro textPower;

    private Vector3 pos;

    private TrackEntry trackEntry;
   // private Animator anim;

   // public List<GameObject> heroList = new List<GameObject> ();

    public bool move;

    
    private void Awake()
    {
        skeletonAnimation =  GetComponentInChildren<SkeletonAnimation>();
        effect = transform.GetChild(2).GetComponent<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
       // skeleton = skeletonAnimation.Skeleton;

        //animationState.End += delegate
        //{
        //    Debug.Log("Play");
        //};        
    }

    void Start()
    {
        pos = transform.position;
       // anim = gameObject.GetComponentInChildren<Animator>();
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
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,new Vector3(0.5f,-0.3f,-2), 6 * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, new Vector2(0.5f, 0)) < 0.001f)
            {
                move = false;
            }
        }

    }

    //public void Attack()
    //{
    //    if (monsterType == MonsterType.NONE)
    //    {
    //       TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_nhay", false);
    //        trackEntry.Complete += AnimAttack;

    //    }
    //    else if (monsterType == MonsterType.RIFLE)
    //    {
    //       TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_ak", false);
    //        trackEntry.Complete += AnimAttack;

    //    }
    //    else if (monsterType == MonsterType.SHOTGUN)
    //    {
    //       TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_shootgun", false);
    //        trackEntry.Complete += AnimAttack;

    //    }
    //    else if (monsterType == MonsterType.LAZE)
    //    {
    //       TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_laser", false);
    //        trackEntry.Complete += AnimAttack;

    //    }

    //}

    public void AnimAttack()
    {
        if (monsterType == MonsterType.NONE)
        {
           trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "skill_sung_luc", false);
            trackEntry.Complete += OnComplete;
        }
        else if (monsterType == MonsterType.RIFLE)
        {
           trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "skill_sung_ak", false);
            trackEntry.Complete += OnComplete;
        }
        else if (monsterType == MonsterType.SHOTGUN)
        {
            trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "skill_sung_shotgun", false);
            trackEntry.Complete += OnComplete;

        }
        else if (monsterType == MonsterType.LAZE)
        {
            trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "skill_sung_laser", false);
            trackEntry.Complete += OnComplete;
        }
    }
    public void SetText()
    {
        textPower.text = powerId.ToString();
    }
    public void ChangePlayer()
    {
        if (monsterType == MonsterType.NONE)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "dung", true);
        }
        else if (monsterType == MonsterType.RIFLE)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "dung_ak", true);
        }
        else if (monsterType == MonsterType.SHOTGUN)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "dung_shootgun", true);
        }
        else if (monsterType == MonsterType.LAZE)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "dung_laser", true);
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
    public void ResetPos()
    {
        transform.position = pos;
    }
    public void AnimHeroDie()
    {
        skeletonAnimation.AnimationState.SetAnimation(1, "lose", true).Delay = 1;
    }
    public void Drag()
    {
        if (monsterType == MonsterType.NONE)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "nhay", true);
        }
        else if (monsterType == MonsterType.RIFLE)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "nhay_ak", true);
        }
        else if (monsterType == MonsterType.SHOTGUN)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "nhay_shootgun", true);
        }
        else if (monsterType == MonsterType.LAZE)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "nhay_laser", true);
        }
        //skeletonAnimation.AnimationState.SetAnimation(1, "nhay", true);
    }
    public IEnumerator Drop()
    {
        if (monsterType == MonsterType.NONE)
        {
            trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_nhay", false);            
        }
        else if (monsterType == MonsterType.RIFLE)
        {
            trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_ak", false);           
        }
        else if (monsterType == MonsterType.SHOTGUN)
        {
            trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_shootgun", false);
        }
        else if (monsterType == MonsterType.LAZE)
        {
            trackEntry = skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_laser", false);            
        }
        trackEntry.Complete += OnComplete;

        //trackEntry =  skeletonAnimation.AnimationState.SetAnimation(1, "tiepdat_nhay", false);
        //trackEntry.Complete += OnComplete;
        yield return new WaitForSpineAnimationComplete(trackEntry);
        //AnimAttack();
    }
    public void OnComplete(TrackEntry trackEntry)
    {
        //Debug.Log("ok");
        if (monsterType == MonsterType.NONE)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "dung", true);
        }
        else if (monsterType == MonsterType.RIFLE)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "dung_ak", true);
        }
        else if (monsterType == MonsterType.SHOTGUN)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, "dung_shootgun", true);
        }
        else if (monsterType == MonsterType.LAZE)
        {
           skeletonAnimation.AnimationState.SetAnimation(1, "dung_laser", true);
        }
        
    }
    public void AnimWin()
    {
        skeletonAnimation.AnimationState.SetAnimation(1, "win", true);
    }
    public void StartEffect(string nameEffect)
    {
        effect.gameObject.SetActive(true);
        TrackEntry trackEntry = effect.AnimationState.SetAnimation(1, nameEffect, false);
        trackEntry.Complete += EndEffect;
    }
    public void EndEffect(TrackEntry trackEntry)
    {
        effect.gameObject.SetActive(false);

    }
}
