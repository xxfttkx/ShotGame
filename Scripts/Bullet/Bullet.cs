using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float existTime = 3f;

    //确保一个子弹只能作用一次
    public bool hit = false;
    public int damage = Settings.bulletDamage;
    private void OnEnable()
    {
        existTime = Settings.bulletExistTime;
        hit = false;
        EventHandler.StartNewGame += OnStartNewGame;
    }
    private void OnDisable()
    {
        EventHandler.StartNewGame -= OnStartNewGame;
    }
    private void Update()
    {
        existTime -= Time.deltaTime;
        if (existTime < 0f)
        {
            EventHandler.CallReleaseBullet(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            if (!hit)
            {
                hit = true;
            }
            else
            {
                return;
            }
            if (other.gameObject.activeInHierarchy)
            {
                var dir = new Vector2();
                if (this.transform.position.x < other.transform.position.x)
                {
                    dir.x = Settings.bulletRepelForce;
                }
                else
                {
                    dir.x = -Settings.bulletRepelForce;
                }
                other.gameObject.GetComponent<Enemy>()?.Hurt(damage, dir);
            }
            if (this.gameObject.activeInHierarchy)
            {
                EventHandler.CallReleaseBullet(this.gameObject);
            }

        }
    }
    void OnStartNewGame()
    {
        PoolManager.Instance.OnReleaseBullet(this.gameObject);
    }
}
