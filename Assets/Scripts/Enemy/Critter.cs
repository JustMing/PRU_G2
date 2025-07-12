using UnityEngine;

public class Critter : Enemy
{
    [SerializeField] private Sprite[] sprites;

    private float moveSpeed;
    private Vector3 targetPosition;

    private float moveTimer;
    private float moveInterval;
    private ObjectPooler zappedEffectPool;
    private ObjectPooler burnEffectPool;

    public override void OnEnable()
    {
        base.OnEnable();
        moveSpeed = Random.Range(0.5f, 3f);
        GenerateRandomPosition();
        moveInterval = Random.Range(0.5f, 2f);
        moveTimer = moveInterval;
    }
    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];

        zappedEffectPool = GameObject.Find("Critter1_ZappedPool").
            GetComponent<ObjectPooler>();
        burnEffectPool = GameObject.Find("Critter1_BurnPool").
            GetComponent<ObjectPooler>();
        destroyEffectPool = GameObject.Find("Critter1_ZappedPool").
            GetComponent<ObjectPooler>();
    }

    public override void Update()
    {
        base.Update();
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
        }
        else
        {
            GenerateRandomPosition();
            moveInterval = Random.Range(0.1f, 2f);
            moveTimer = moveInterval;
        }

        transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, moveSpeed * Time.deltaTime);
    }

    public void SpawnSine()
    {
        base.Update();
        float sine = Mathf.Sin(transform.position.y);
        transform.position = new Vector3(transform.position.x, sine);
    }

    private void GenerateRandomPosition()
    {
        float randomX = Random.Range(-9f, 9f);
        targetPosition = new Vector2(randomX, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject burnEffect = burnEffectPool.GetPooledObject();
            burnEffect.transform.position = transform.position;
            burnEffect.transform.rotation = transform.rotation;
            burnEffect.SetActive(true);
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(1);
        }
    }

}
