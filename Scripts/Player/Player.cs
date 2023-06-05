using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed;
    private float inputX;
    private float inputY;
    private Vector2 movementInput;
    public FireType fireType;
    public BoxCollider2D footBoxCollider2D;
    private Animator anim;
    private float slowTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = Settings.playerSpeed;
        jumpForce = Settings.playerJumpForce;
    }
    void Start()
    {

    }
    private void OnEnable()
    {
        EventHandler.StartNewGame += OnStartNewGame;
        EventHandler.HurtPlayer += OnHurtPlayer;
    }
    private void OnDisable()
    {
        EventHandler.StartNewGame -= OnStartNewGame;
        EventHandler.HurtPlayer -= OnHurtPlayer;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.gameState != GameState.GamePlay) return;
        if (slowTime > 0)
        {
            slowTime -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (fireType == FireType.Single)
                fireType = FireType.Triple;
            else if (fireType == FireType.Triple)
                fireType = FireType.Single;
        }
        if (Input.GetMouseButtonDown(0))
        {
            slowTime = Settings.shootSlowTime;
            Shoot();
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (CheckIsOnGround())
                // rb.AddForce(new Vector2(0, test * Time.deltaTime));
                Jump();
        }
    }
    private void FixedUpdate()
    {
        PlayerInput();
        Movement();
    }
    private void PlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(inputX, inputY);
    }
    private void Movement()
    {
        anim.SetBool("isMoving", inputX != 0);
        if (slowTime > 0)
        {
            rb.velocity = new Vector2(inputX * Settings.shootSlowSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        }
        
        // rb.AddForce(new Vector2(movementInput.x, inputY * 10) * speed);1
        // rb.MovePosition(rb.position + movementInput * speed);
    }
    public float jumpForce = 0.1f;
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // rb.AddForce(Vector2.up * test);

    }
    private bool CheckIsOnGround()
    {
        if (footBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return true;
        else
            return false;
    }

    void OnStartNewGame()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            if (rb.velocity.y < 0)
            {
                if (collision.otherCollider != footBoxCollider2D)
                {
                    return;
                }
                // collision.gameObject.GetComponent<Enemy>()?.Hurt(5, Vector2.zero);
                Jump();
            }
            else
            {
                //TODO : 修改damage
                // EventHandler.CallHurtPlayer(collision.transform.position, 1);
            }
        }
    }
    public int testForce = 10;
    void OnHurtPlayer(Vector3 enemyPos, int damage)
    {
        // rb.AddForce(testForce * (transform.position - enemyPos));
    }

    void Shoot()
    {
        var mainCamera = Camera.main;
        var mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
        var dir = mouseWorldPos - this.transform.position;
        var randomShifting = Random.Range(-Settings.maxRandomShifting, Settings.maxRandomShifting);
        Vector2 force;
        Vector2 gunFirePos = this.transform.position;
        if (dir.x > 0)
        {
            force = new Vector2(Settings.bulletSpeed, randomShifting);
            gunFirePos.x += Settings.playerWidth / 2;
        }
        else
        {
            force = new Vector2(-Settings.bulletSpeed, randomShifting);
            gunFirePos.x -= Settings.playerWidth / 2;
        }
        if (fireType == FireType.Single)
        {
            EventHandler.CallCreateBullet(this.transform.position, force);
        }
        else if (fireType == FireType.Triple)
        {
            var upShootForce = force;
            var downShootForce = force;
            upShootForce.y += Settings.TripleShootShifting;
            downShootForce.y -= Settings.TripleShootShifting;
            EventHandler.CallCreateBullet(this.transform.position, upShootForce);
            EventHandler.CallCreateBullet(this.transform.position, force);
            EventHandler.CallCreateBullet(this.transform.position, downShootForce);
        }
        EventHandler.CallCreateGunFire(gunFirePos);
        var soundDetails = AudioManager.Instance.GetSoundDetailsBySoundName(SoundName.Shoot);
        EventHandler.CallCreateSound(soundDetails);
    }
}
