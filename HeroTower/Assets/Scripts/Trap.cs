using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public TrapType trapType;
    public int powerId;
    public TextMeshPro textPower;

    public SkeletonAnimation skeletonAnimation;

    public Spine.AnimationState animationState;
    private void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
    }
    void Start()
    {
        textPower = transform.GetChild(0).GetComponent<TextMeshPro>();

        textPower.text = "-" + powerId.ToString();
    }
    public void Trapping()
    {
        if (trapType == TrapType.FOLD)
        {
          TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(1,"bay_open",false);
            trackEntry.Complete += DestroyTrap;
        }else if (trapType == TrapType.THORN)
        {
            TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(1,"chong_open", false);
            trackEntry.Complete += DestroyTrap;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void DestroyTrap(TrackEntry trackEntry)
    {
        Destroy(gameObject);
    }
  
}
public enum TrapType
{
    NONE,
    FOLD,
    THORN
}
