﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public float HP = 1.0f;
    public AudioClip collectedClip;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController rubyCtrl = other.GetComponent<RubyController>();
        if (rubyCtrl != null && rubyCtrl.ChangeHealth(HP))
        {
            rubyCtrl.invincibleTimer = 3;
            Destroy(gameObject);
            rubyCtrl.PlaySound(collectedClip);
            Debug.Log("Object that entered the trigger : " + rubyCtrl.gameObject);

        }
    }
}
