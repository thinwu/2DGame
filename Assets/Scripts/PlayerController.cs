using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MovableObj
{
    public GameObject swordWind;
    Animator animator;
    private enum Attack
    {
        ExitAttack = 0, Melee = 1,Cast,RapidMelee,Strike

    }
    private void Awake()
    {
        PreSetup();
        animator = GetComponent<Animator>();
        animator.SetFloat(base.AnimiLookX, direction.x);
    }
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        PlayerMove(input);
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
}
