using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called before the first frame update
    private RubyController rubyCtrl;
    public float damage = -.3f;
    private bool inDamage = false;
    public float damageDuration = 2.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        rubyCtrl = other.GetComponent<RubyController>();
        inDamage = true;
        while (rubyCtrl != null && inDamage)
        {
            rubyCtrl.ChangeHealth(damage);
            rubyCtrl.invincibleTimer = damageDuration;
            yield return new WaitForSeconds(damageDuration);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        rubyCtrl = other.GetComponent<RubyController>();
        if (rubyCtrl != null)
        {
            inDamage = false;

        }
    }
}
