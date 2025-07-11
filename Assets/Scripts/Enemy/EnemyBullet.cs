using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField]private float force;
    public int damage;

     void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        Vector3 direction = player.transform.position - transform.position;
        if (rb) rb.linearVelocity = new Vector2(direction.x * Time.deltaTime, 
                                    direction.y * Time.deltaTime).normalized * force;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            if (rb) rb.linearVelocity = new Vector2(direction.x * Time.deltaTime,
                                direction.y * Time.deltaTime).normalized * force;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0, 0)); 
        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        if((transform.position.x < min.x) || (transform.position.x > max.x) ||
            (transform.position.y < min.y) || (transform.position.y > max.y))
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        
    }
}
