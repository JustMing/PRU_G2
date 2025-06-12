using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 mousePos;
    [SerializeField] private Camera cam;
    [SerializeField] private float offSet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float rotateAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offSet;
        rb.rotation = rotateAngle;
    }
}
