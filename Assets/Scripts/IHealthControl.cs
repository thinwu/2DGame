using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthControl
{
    bool ChangeHealth(float amount, float invincibleTimer, ref float currentHealth, float maxHealth);
}
