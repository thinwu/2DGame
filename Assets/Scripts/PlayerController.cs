using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DamageControl))]
public class PlayerController : MovableObj
{
    public ParticleSystem PowerUp;
    public GameObject swordWind;
    public int swordWindCount = 0;
    public float swordWindSpeed = 400f;
    public float swordWindInterval = 0.07f;
    public float swordWindOffsetScale = 0.5f;


    private Animator animator;
    private Vector2 input;
    private readonly string AnimiBlock = "Block";
    
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
    protected IEnumerator LaunchSwordWind()
    {
        Vector2 location = transform.position;
        for(int i = 1; i<=swordWindCount; i++)
        {
            GameObject projectileObject = Instantiate(swordWind, location + (Vector2.up + direction) * swordWindOffsetScale, Quaternion.identity);
            Bullet b = projectileObject.GetComponent<Bullet>();
            b.Launch(direction, swordWindSpeed);
            yield return new WaitForSeconds(swordWindInterval);
        }
        
    }
} 
