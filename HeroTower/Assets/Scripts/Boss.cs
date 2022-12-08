using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Spine.Unity;

public class Boss : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState animationState;
    public Spine.Skeleton skeleton;

    public int powerId;
    private TextMeshPro textPower;
    private Animator anim;
    private void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        textPower = transform.GetChild(0).GetComponent<TextMeshPro>();

        textPower.text = powerId.ToString();
    }
    public void AnimAttack()
    {
        anim.SetTrigger("Attack");
        
    }
    public void BossMove()
    {
        transform.DOMoveX(transform.position.x + 4,1);
    }
    public void AnimBossDie()
    {
        anim.SetTrigger("Attack");

    }
}
