using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float pushX = Random.Range(-1f, 1f);
        float pushY = Random.Range(-1f, 0);
        rb.linearVelocity = new Vector2(pushX, pushY);
    }

    
    void Update()
    {
        float moveY = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);
        if(transform.position.y < -13)
        {
            Destroy(gameObject);
        }
    }
}
