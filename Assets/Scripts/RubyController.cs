using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : HealthControl
{
    // Start is called before the first frame update
    public float speed = 5.0f;

    public float maxHealth = 100;
    public GameObject[] bullet;
    [HideInInspector]
    public float invincibleTimer = 2f;
    private float currentHealth;
    private float spriteBlinkingTotalTimer = 0;
    private float spriteBlinkingTimer = 0;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    [HideInInspector]
    public int bulletCount;
    private float spriteBlinkingMiniDuration = .05f;
    Vector2 lookDirection = new Vector2(1, 0);
    private Vector2 touchOriginPoint = -Vector2.one;
    private Vector2 touchEndPoint = -Vector2.one;
    int currentBullet = 0;
    private float width;
    private float height;
    AudioSource audioSource;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        audioSource = GetComponent<AudioSource>();
    }
    Vector2 TouchToMove(Touch touchSession, ref Vector2 touchOriginPoint, ref Vector2 touchEndPoint) 
    { 
    
        if (touchSession.phase == TouchPhase.Began)
        {
            touchOriginPoint = touchSession.position;
        }
        else if(touchSession.phase == TouchPhase.Stationary || touchSession.phase == TouchPhase.Moved)
        {
            touchEndPoint = touchSession.position;
        }

        if (touchEndPoint != -Vector2.one && touchOriginPoint != -Vector2.one)
        {
            Vector2 pos = (touchEndPoint - touchOriginPoint);

            pos.x = Mathf.Clamp(pos.x / 150, -0.4f, 0.4f);
            pos.y = Mathf.Clamp(pos.y / 150, -0.4f, 0.4f);
            return pos;
        }
        return Vector2.zero;

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
#if UNITY_STANDALONE || UNITY_WEBPLAYER
#else
        foreach (Touch t in Input.touches)
        {
            
            if (t.position.x < Screen.width / 2)
            {
                move = TouchToMove(t, ref touchOriginPoint, ref touchEndPoint);
            }
            else if (t.position.x > Screen.width / 2)
            {
                if(t.tapCount > 0 && t.tapCount <= 3 && t.phase == TouchPhase.Began)
                {
                    Launch();
                }
            }
        }

#endif

        if (Input.GetKeyDown(KeyCode.X))
        {
            tryTalkToNPC();
        }
        Vector2 position = rigidbody2d.position;
        float deltaT = Time.deltaTime;

        position = position + move * speed * Time.deltaTime;

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        rigidbody2d.MovePosition(position);
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            SpriteBlinkingEffect();
        }

    }
    public bool ChangeHealth(float amount)
    {
        if (base.ChangeHealth(amount, invincibleTimer, ref currentHealth, maxHealth))
        {
            HealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            return true;
        }
        return false;
        
    }
    public void ChangeBullet(int amount)
    {
        bulletCount += amount;
        HealthBar.instance.SetBullet(bulletCount);
    }
    private void SpriteBlinkingEffect()
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
    void tryTalkToNPC()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            JambiController jambiCtrl = hit.collider.GetComponent<JambiController>();
            if(jambiCtrl != null)
            {
                jambiCtrl.DisplayDialog();
            }

        }
    }
    void Launch()
    {
        if (bullet.Length > 0 && bulletCount>0)
        {
            ChangeBullet(-1);
            GameObject projectileObject = Instantiate(bullet[currentBullet], rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Bullet b = projectileObject.GetComponent<Bullet>();
            b.Launch(lookDirection, 300);
        }

    }
}
