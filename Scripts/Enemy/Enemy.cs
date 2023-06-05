using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed = 5;
    public int healthPoint = Settings.midoriHealth;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Coroutine highLight;
    private Animator anim;
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        healthPoint = Settings.midoriHealth;
        sp.color = Color.white;
        EventHandler.StartNewGame += OnStartNewGame;
    }
    private void OnDisable()
    {
        EventHandler.StartNewGame -= OnStartNewGame;
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector2 dir = new Vector2();
        if (Math.Abs(this.transform.position.x - playerTransform.position.x) < 0.1f)
        {
            anim.SetBool("isMoving",false);
        }
        else
        {
            if (this.transform.position.x > playerTransform.position.x)
            {
                dir.x = -1;
            }
            else
            {
                dir.x = 1;
            }

            rb.AddForce(dir * speed, ForceMode2D.Force);
            anim.SetBool("isMoving", true);
        }
        
    }

    public void Hurt(int damage, Vector2 dir)
    {
        healthPoint -= damage;
        rb.AddForce(dir);
        if (highLight != null)
        {
            StopCoroutine(highLight);
        }
        highLight = StartCoroutine(HighLightEnemy());
        if (healthPoint <= 0)
        {
            //爆炸
            EventHandler.CallCreateBoom(this.transform.position);
            StartCoroutine(ExplodeOtherEnemy());
            
            //声音
            var soundDetails = AudioManager.Instance.GetSoundDetailsBySoundName(SoundName.Boom);
            EventHandler.CallCreateSound(soundDetails);
        }
    }
    IEnumerator HighLightEnemy()
    {
        var sp = this.GetComponent<SpriteRenderer>();
        if (sp != null)
        {
            sp.color = Color.red;
            yield return new WaitForSeconds(Settings.hitHighLightTime);
            sp.color = Color.white;
        }
    }
    IEnumerator ExplodeOtherEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, Settings.explodeRange);
        foreach (var coll in colliders)
        {
            if (coll.gameObject == this.gameObject) continue;
            if(coll.CompareTag("Player"))
            {
                EventHandler.CallEndGame();
                yield break;
            }
            if (coll.CompareTag("Enemy"))
            {
                coll.GetComponent<Enemy>()?.Hurt(10, Vector2.zero);
            }
        }
        EventHandler.CallKillEnemy(this.gameObject);
    }
    void OnStartNewGame()
    {
        // this.gameObject.SetActive(false);
        PoolManager.Instance.ReleaseEnemy(this.gameObject);
    }
}