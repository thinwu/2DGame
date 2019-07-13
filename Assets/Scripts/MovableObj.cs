using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2DScroller))]
public class MovableObj : MonoBehaviour
{
    public float jumpHeight = 3.5f;
    public float timeToJumpApex = .3f;
    public float moveSpeed = 6;
    protected float accelerationTimeAirborne = .2f;
    protected float accelerationTimeGrounded = .1f;
    protected float velocityXSmoothing;
    protected float jumpVelocity;
    protected float gravity;
    protected Controller2DScroller controller;
    protected Vector3 velocity;
    protected Vector2 direction = new Vector2(1, 0);
    protected string AnimiJumpUp = "JumpUp";
    protected string AnimiLookX = "LookX";
    protected string AnimiSpeedY = "SpeedY";
    protected string AnimiSpeed = "Speed";

    protected void PreSetup()
    {
        controller = GetComponent<Controller2DScroller>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        Debug.Log("gravity: " + gravity + " jumpVelocity: " + jumpVelocity);
    }
    protected void SimpleMove(Vector2 input, Animator animator, bool PressedJump)
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            animator.SetBool(AnimiJumpUp, false);
        }

        if (PressedJump && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            animator.SetBool(AnimiJumpUp, true);
        }
        float targetVelocityX = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne);


        if (controller.collisions.left || controller.collisions.right)
        {
            velocity.x = 0;
        }
        direction.x = Mathf.Sign(velocity.x);
        animator.SetFloat(AnimiLookX, direction.x);
        animator.SetFloat(AnimiSpeedY, Mathf.Abs(velocity.y));
        animator.SetFloat(AnimiSpeed, Mathf.Abs(velocity.x));
        controller.Move(velocity * Time.deltaTime);
    }
}
