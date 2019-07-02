using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0f;

    public float maxHealth = 100;
    [HideInInspector]
    public float invincibleTimer = 2f;
    public float CurrentHealth { get; private set; }

    private float spriteBlinkingTotalTimer = 0;
    private float spriteBlinkingTimer = 0;
    private Rigidbody2D rigidbody2d;

    private float spriteBlinkingMiniDuration = .05f;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            SpriteBlinkingEffect();
        }

    }
    public void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            if (invincibleTimer > 0)
                return;
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
        Debug.Log(CurrentHealth + "/" + maxHealth);
        invincibleTimer = 0f;
    }
    private void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        Color tmp = this.gameObject.GetComponent<SpriteRenderer>().color;
        if (spriteBlinkingTotalTimer >= invincibleTimer)
        {
            spriteBlinkingTotalTimer = 0.0f;
            tmp.a = 1f;
            this.gameObject.GetComponent<SpriteRenderer>().color = tmp;   // according to 
                                                                             //your sprite
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            tmp.a = (gameObject.GetComponent<SpriteRenderer>().color.a < 1f) ? 1f : .5f;
            this.gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
    }
}
