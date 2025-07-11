using UnityEngine;

public class Boss : MonoBehaviour
{

    private int heath;
    private Animator animator;
    private float speedX;
    private float speedY;
    private bool isMoving;
    private FlashWhite flashWhite;

    private float switchInterval;
    private float switchTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heath = 100;
        animator = GetComponent<Animator>();
        flashWhite = GetComponent<FlashWhite>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            if (isMoving)
            {
                SpawnState();
            }
            else
            {
                MovingState();
            }
        }

        if (transform.position.x <= -7f || transform.position.x >= 7f)
        {
            speedX *= -1;
        }
        if (transform.position.y <= 1f || transform.position.y >= 3.5f)
        {
            speedY *= -1;
        }
        transform.position += new Vector3(speedX, speedY, 0) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        heath -= damage;
        flashWhite.Flash(); 
    }

    private void MovingState()
    {
        if (transform.position.y <= 1f || transform.position.y >= 2.5f)
        {
            speedY *= -1;
        }
        speedX = Random.Range(-3f, 3f);
        speedY = Random.Range(-3f, 3f);
        switchInterval = Random.Range(1f, 3f);
        switchTimer = switchInterval;
        isMoving = true;
        animator.SetBool("isAttack", false);
        animator.SetBool("isSpawn", false);
    }

    private void SpawnState()
    {
        speedX = 0f;
        speedY = 0f;
        switchInterval = Random.Range(1f, 3f);
        switchTimer = switchInterval;
        isMoving = false;
        animator.SetBool("isSpawn", true);
    }
}
