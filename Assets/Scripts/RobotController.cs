using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float speed = 3f;
    public bool vertical;
    public ParticleSystem smokeEffect;
    // Start is called before the first frame update
    private Rigidbody2D rigidbody2d;
    public float changeTime = 2.0f;
    Animator animator;
    float timer;
    int direction = 1;
    bool broken = true;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken) return;
        
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("X", direction);
            animator.SetFloat("Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            player.invincibleTimer = 1.0f;
        }
    }
    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        if(smokeEffect != null) Destroy(smokeEffect);
    }

}
