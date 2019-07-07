using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxExplosive : HealthControl
{
    public ParticleSystem burst;
    public float maxHealth = 3;
    private float currentHealth;
    private Rigidbody2D rigidbody2d;
    public float invincibleTimer = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Bullet b = other.gameObject.GetComponent<Bullet>();
        if (b != null)
        {
            Debug.Log(currentHealth);

            base.ChangeHealth(b.damage, invincibleTimer, ref currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                Vector2 position = gameObject.transform.position;
                GameObject.Instantiate(burst, position + Vector2.up * 0.5f, Quaternion.identity);
            }
        }
        
    }

}
