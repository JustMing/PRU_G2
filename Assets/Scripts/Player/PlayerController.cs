
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private Animator animator;
    private FlashWhite flashWhite;

    [SerializeField] private float moveSpeed;
    //public Vector2 _velocity = new (0, 0);

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    private ObjectPooler destroyEffectPool;

    [SerializeField] private int experience;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevel;
    [SerializeField] private List<int> playerLevels;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashWhite = GetComponent<FlashWhite>();
        destroyEffectPool = GameObject.Find("PlayerBoomPool").
            GetComponent<ObjectPooler>();

        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.1f + 15));
        }

            health = maxHealth;
        UIController.Instance.UpdateHealthSlider(health,maxHealth);
        experience = 0;
        UIController.Instance.UpdateExpSlider(experience, playerLevels[currentLevel]);
    }

    
    void Update()
    {
        if (Time.timeScale > 0)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");
            playerDirection = new Vector2(directionX, directionY).normalized;
            animator.SetFloat("MoveX", directionX);
            animator.SetFloat("MoveY", directionY);

            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetButtonDown("Fire1"))
            {
                ShipWeapon.Instance.Shoot();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed,
            playerDirection.y * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid) asteroid.TakeDamage(1,false);
        }else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy) enemy.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        AudioManager.Instance.PlaySound(AudioManager.Instance.hit);
        flashWhite.Flash();
        if(health <= 0)
        {
            GameManager.Instance.SetWorldSpeed(0f);
            gameObject.SetActive(false);
            //Instantiate(destroyEffect,transform.position,transform.rotation);
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);
            AudioManager.Instance.PlaySound(AudioManager.Instance.shipExplosion);
            Time.timeScale = 0;
        }
    }

    public void GetExperience(int exp)
    {
        experience += exp;
        UIController.Instance.UpdateExpSlider(experience, playerLevels[currentLevel]);
        if(experience > playerLevels[currentLevel])
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        experience -= playerLevels[currentLevel];
        if(currentLevel < maxLevel - 1)
        {
            currentLevel++;
        }
        if (Random.Range(0, 10) % 2 == 0)
        {
            health += 1;
        }
        UIController.Instance.UpdateExpSlider(experience, playerLevels[currentLevel]);
        ShipWeapon.Instance.LevelUp();
    }

    public void GetScorePoint(int score)
    {
        UIController.Instance.AddPoint(score);
    }
}
