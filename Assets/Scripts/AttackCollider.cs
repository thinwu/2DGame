using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public float damage;
    public float pushDistance = 30;
    // Start is called before the first frame update
    void Awake()
    {
        damage  = damage * gameObject.GetComponentInParent<PlayerController>().generalDamageScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageControl damageCtrl = other.GetComponent<DamageControl>();
        if (damageCtrl != null)
        {
            damageCtrl.ChangeHealth(damage);
        }
    }
}
