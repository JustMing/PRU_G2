using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private Animator animator;
    [SerializeField] private float moveSpeed;
    //public Vector2 _velocity = new (0, 0);
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2 (directionX, directionY).normalized;
        animator.SetFloat("MoveX", directionX);
        animator.SetFloat("MoveY", directionY);
        //_velocity = HandleHorizontal(Input.GetAxis("Horizontal"));
        //_velocity += HandleVertical(Input.GetAxis("Vertical"));
        //MoveShip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed,
            playerDirection.y * moveSpeed);
    }

    //private Vector2 HandleHorizontal(float x)
    //{
    //    return new Vector2(Mathf.Clamp(x, -1, 1), 0);
    //}

    //private Vector2 HandleVertical(float y)
    //{
    //    return new Vector2(0,Mathf.Clamp(y, -1, 1));
    //}

    //private void MoveShip()
    //{
    //    float newX = transform.position.x + (_velocity.x * speed * Time.deltaTime);
    //    float newY = transform.position.y + (_velocity.y * speed * Time.deltaTime);
    //    transform.position = new Vector2(newX,newY);
    //}
}
