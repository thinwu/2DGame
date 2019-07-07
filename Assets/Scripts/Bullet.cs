using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    private float timeinterval = 2.0f;
    public float damage = -1.0f;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }
    public void Launch(Vector2 direction, float force)
    {
        
        rigidbody2d.AddForce(direction * force);
        Invoke("destoryInTime" , timeinterval);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RobotController e = other.collider.GetComponent<RobotController>();
        if (e != null)
        {
            e.Fix();
        }

        Destroy(gameObject);
        Debug.Log("Collide with: " + other);
        Destroy(gameObject);
    }

    private void destoryInTime()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
