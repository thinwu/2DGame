using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    public int bulletCount = 30;
    public GameObject Bullet;
    private Vector2 location;
    void Start()
    {
        location = gameObject.transform.position;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D: " + other);
        RubyController rubyCtrl = other.GetComponent<RubyController>();
        if(rubyCtrl != null)
        {
            for (int i = 0; i < rubyCtrl.bullet.Length; i++)
            {
                if(rubyCtrl.bullet[i] == null)
                {
                    rubyCtrl.bullet[i] = Bullet;
                    rubyCtrl.ChangeBullet(bulletCount);
                    Vector2 point = gameObject.transform.position;
                    GameManager.instance.SpawnInSec(GameManager.instance.Ammos[0], point, 2.0f);
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
