using UnityEngine;

public class Battlecruiser : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private float timer;
    private float frequency;
    private float amplitude;
    private float centerX;
    [SerializeField] private GameObject bulletPosition;
    [SerializeField] private ObjectPooler bulletPool;
    private float setBulletTime;
    private GameObject player;


    public override void OnEnable()
    {
        base.OnEnable();
        timer = transform.position.x;
        frequency = Random.Range(0.3f,1f);
        amplitude = Random.Range(0.3f,1.5f);
        centerX = transform.position.x;
    }

    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        destroyEffectPool = GameObject.Find("BattlecruiseBoomPool").GetComponent<ObjectPooler>();
        bulletPool = GameObject.Find("EnemyBulletPool").GetComponent<ObjectPooler>();
        speedY = Random.Range(-0.8f, -1.5f);
    }

    public override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
        float sine = Mathf.Sin(timer * frequency) * amplitude;
        transform.position = new Vector3(sine + centerX, transform.position.y);
        setBulletTime += Time.deltaTime;
        if (setBulletTime > 1.5f)
        {
            setBulletTime = 0;
            EnemyShoot();
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
