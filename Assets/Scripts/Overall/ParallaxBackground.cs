using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float backgroundImageHeight;


    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundImageHeight = sprite.texture.height / sprite.pixelsPerUnit;
    }

    
    void Update()
    {
        float moveY = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0, moveY);
        if (Mathf.Abs(transform.position.y) - backgroundImageHeight > 0)
        {
            transform.position = new Vector3(transform.position.x,0f);
        }
    }
}
