using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MovableObj
{

    private Transform target;
    Animator animator;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        PreSetup();
        animator = GetComponent<Animator>();
        animator.SetFloat(base.AnimiLookX, direction.x);
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 input = new Vector2( Mathf.Sign(target.position.x - gameObject.transform.position.x),0);
        SimpleMove(input, animator, true);
    }
}
