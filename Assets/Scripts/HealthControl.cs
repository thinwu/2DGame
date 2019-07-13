using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
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
}
