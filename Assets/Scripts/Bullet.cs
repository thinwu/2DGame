using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    public float timeinterval = 2.0f;
    public float damage = -1.0f;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        gameObject.GetComponent<SpriteRenderer>().flipX = (direction.x==-1)?true:false;
        Invoke("destoryInTime" , timeinterval);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageControl damageCtrl = other.collider.GetComponent<DamageControl>();
        if(damageCtrl != null)
        {
            damageCtrl.ChangeHealth(damage);
            Destroy(gameObject);
        }
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
