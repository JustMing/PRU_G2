using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        float moveY = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);
        if (transform.position.y < -13)
        {
            Destroy(gameObject);
        }
    }
}
