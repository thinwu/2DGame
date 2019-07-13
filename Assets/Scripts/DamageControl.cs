using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour, IHealthControl
{
    public float spriteBlinkingTotalTimer = 2f;
    public float spriteBlinkingMiniDuration = .05f;
    public float invincibleTimer = 2f;
    public float maxHealth = 1;

    private float spriteBlinkingTimer = 0;
    protected float currentHealth;

    private void Update()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            SpriteBlinkingEffect();
        }
    }
    public bool ChangeHealth(float amount, float invincibleTimer, ref float currentHealth, float maxHealth)
    {
        if (amount < 0)
        {
            if (invincibleTimer > 0)
                return false;
        }
        if (amount > 0 && currentHealth >= maxHealth) return false;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        invincibleTimer = 0f;
        return true;
    }
    public bool ChangeHealth(float amount)
    {
        if (ChangeHealth(amount, invincibleTimer, ref currentHealth, maxHealth))
        {
            CheckDamageToDie();
            return true;
        }
        return false;
    }
    protected void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        Color tmp = this.gameObject.GetComponent<SpriteRenderer>().color;
        if (spriteBlinkingTotalTimer >= invincibleTimer)
        {
            spriteBlinkingTotalTimer = 0.0f;
            tmp.a = 1f;
            this.gameObject.GetComponent<SpriteRenderer>().color = tmp;
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
    protected void CheckDamageToDie()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
