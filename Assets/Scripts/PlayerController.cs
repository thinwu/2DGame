using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DamageControl))]
public class PlayerController : MovableObj
{
    public GameObject swordWind;
    Animator animator;
    Vector2 input;

    private string AnimiBlock = "Block";
    private enum Attack
    {
        ExitAttack = 0, Melee = 1,Cast,RapidMelee,Strike

    }
    private void Awake()
    {
        PreSetup();
        AnimiJumpUp = "JumpUp";
        AnimiLookX = "LookX";
        AnimiSpeedY = "SpeedY";
        AnimiSpeed = "Speed";
        animator = GetComponent<Animator>();
        animator.SetFloat(base.AnimiLookX, direction.x);

    }
    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        PlayerMove(input);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Block();
        }
    }
    private void PlayerMove(Vector2 input)
    {
        animator.SetFloat("Attack", (float)Attack.ExitAttack);
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetFloat("Attack", (float)Attack.Melee);
            LaunchSwordWind(); 
        }
        base.SimpleMove(input, animator, Input.GetKeyDown(KeyCode.Space));
    }
    protected override void OnHit(RaycastHit2D hit)
    {
        base.OnHit(hit);
        Jumper jumper = hit.collider.GetComponent<Jumper>();
        if(jumper != null && collisionInfo.below)
        {
            base.SimpleMove(input, animator, true);
            jumper.gameObject.GetComponent<DamageControl>().ChangeHealth(-1);
        }
    }
    protected void Block()
    {
        animator.SetBool(AnimiBlock, true);
        
        Invoke("ReleaseBlock", 1);
    }
    protected void ReleaseBlock()
    {
        animator.SetBool(AnimiBlock, false);
    }
    protected void LaunchSwordWind()
    {
        Vector2 location = transform.position;
        GameObject projectileObject = Instantiate(swordWind, location + Vector2.up * 0.5f, Quaternion.identity);
        Bullet b = projectileObject.GetComponent<Bullet>();
        b.Launch(direction, 300);
    }
} 
