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
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed,
            playerDirection.y * moveSpeed);
    }
    
}
