
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;
    protected ObjectPooler destroyEffectPool;
    private GameObject player;

    private int lives;
    [SerializeField] private int maxLives;
    [SerializeField] private int damage;
    [SerializeField] private int givenExp;
    [SerializeField] private int givenPoint;

    protected float speedX = 0;
    protected float speedY = 0;

    [SerializeField] private GameObject bulletPosition;
    [SerializeField] private ObjectPooler bulletPool;
    private float setBulletTime;

    public virtual void OnEnable()
    {
        lives = maxLives;
    }

    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();
        bulletPool = GameObject.Find("EnemyBulletPool").GetComponent<ObjectPooler>();

    }


    public virtual void Update()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);

        setBulletTime += Time.deltaTime;
        if(setBulletTime > 1.5f)
        {
            setBulletTime = 0;
            EnemyShoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>(); 
            if(player) player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;

        if(lives > 0)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.hitObstacle);
            flashWhite.Flash();
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.explosionDestroy);
            flashWhite.Reset();
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);
           
            PlayerController.Instance.GetExperience(givenExp);
            PlayerController.Instance.GetScorePoint(givenPoint);

            gameObject.SetActive(false); 
        }
    }

    public void EnemyShoot()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameObject bullet = bulletPool.GetPooledObject();
            float xPos = bulletPosition.transform.position.x;
            bullet.transform.position = new Vector2(xPos, bulletPosition.transform.position.y);
            bullet.SetActive(true);
        }
    }
}
