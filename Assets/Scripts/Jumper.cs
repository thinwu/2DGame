using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageControl))]
public class Jumper : MovableObj
{

    private Transform target;
    Animator animator;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        PreSetup();
        animator = GetComponent<Animator>();
        AnimiJumpUp = "JumpUp";
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 input = new Vector2( Mathf.Sign(target.position.x - gameObject.transform.position.x),0);
        SimpleMove(input, animator, false);
    }
    protected override void OnHit(RaycastHit2D hit)
    {
        base.OnHit(hit);
        PlayerController player = hit.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            if (collisionInfo.below)
            {
                //player.ChangeHealth(-generalDamageScale);
            }
        }
    }
}
