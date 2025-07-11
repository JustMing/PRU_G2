using UnityEngine;

public class FloatInSpace : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    void Update()
    {
        float moveY = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(0, -moveY*speed);
        if (transform.position.y < -12)
        {
            gameObject.SetActive(false);
        }
    }
}
