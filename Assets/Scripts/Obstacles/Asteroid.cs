using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private Rigidbody2D rb;
    private FlashWhite flashWhite;

    [SerializeField] private ObjectPooler destroyEffectPool;
    [SerializeField] private int maxLives =3;
    private int lives;
    [SerializeField] private int damage = 1;
    [SerializeField] private int givenExp = 1;
    [SerializeField] private int point = 100;

    private float pushX;
    private float pushY;

    private void OnEnable()
    {
        lives = maxLives;
        transform.rotation = Quaternion.identity;
        pushX = Random.Range(-1f, 1f);
        pushY = Random.Range(-1f, 0);
       if(rb) rb.linearVelocity = new Vector2(pushX, pushY);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flashWhite = GetComponent<FlashWhite>();
        destroyEffectPool = GameObject.Find("boom2Pool").GetComponent<ObjectPooler>();

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
         pushX = Random.Range(-1f, 1f);
         pushY = Random.Range(-1f, 0);
        if(rb) rb.linearVelocity = new Vector2(pushX, pushY);
        float randomScale = Random.Range(1f, 2f);
        transform.localScale = new Vector2(randomScale, randomScale);

        lives = maxLives;
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage, bool giveExp)
    {
        lives -= damage;
        if (lives > 0) 
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.hitObstacle);
            flashWhite.Flash();         
        }
       else
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.explosionDestroy);
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);
            //Instantiate(destroyEffect, transform.position, transform.rotation);
            flashWhite.Reset();
            gameObject.SetActive(false);
            if(giveExp) PlayerController.Instance.GetExperience(givenExp);
            PlayerController.Instance.GetScorePoint(point);
        }
    }

}
