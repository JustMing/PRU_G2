using UnityEngine;

public class Critter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private float moveSpeed;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private float moveTimer;
    private float moveInterval;
    private ObjectPooler zappedEffectPool;
    private ObjectPooler burnEffectPool;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];

        zappedEffectPool = GameObject.Find("Critter1_ZappedPool").
            GetComponent<ObjectPooler>();
        burnEffectPool = GameObject.Find("Critter1_BurnPool").
            GetComponent<ObjectPooler>();

        moveSpeed = Random.Range(0.5f, 3f);
        GenerateRandomPosition();
        moveInterval = Random.Range(0.5f, 2f);
        moveTimer = moveInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTimer > 0)
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

        //Vector3 relativePos = targetPosition - transform.position;
        //if (relativePos != Vector3.zero)
        //{
        //    targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
        //        180 * Time.deltaTime);
        //}

        //Vector3 horizontalDirection = new Vector3(targetPosition.x - transform.position.x, 0, 0);
        //if (horizontalDirection.magnitude > 0.1f)
        //{
        //    float angle = Mathf.Atan2(horizontalDirection.x, 1) * Mathf.Rad2Deg;
        //    // Limit the rotation to a small range so it mostly faces upward
        //    angle = Mathf.Clamp(angle, -30f, 30f);
        //    transform.rotation = Quaternion.Euler(0, 0, -angle);
        //}
    }

    private void GenerateRandomPosition()
    {
        float randomX = Random.Range(-9f, 9f);
        //float randomY = Random.Range(-1, 1f);
        targetPosition = new Vector2(randomX, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //Instantiate(zappedEffect, transform.position, transform.rotation);
            GameObject zappedEffect = zappedEffectPool.GetPooledObject();
            zappedEffect.transform.position = transform.position;
            zappedEffect.transform.rotation = transform.rotation;
            zappedEffect.SetActive(true);

            gameObject.SetActive(false);
        }else if (collision.gameObject.CompareTag("Player"))
        {
            //Instantiate(burnEffect, transform.position, transform.rotation);
            GameObject burnEffect = burnEffectPool.GetPooledObject();
            burnEffect.transform.position = transform.position;
            burnEffect.transform.rotation = transform.rotation;
            burnEffect.SetActive(true);
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(1);
            gameObject.SetActive(false);
        }
    }
}
