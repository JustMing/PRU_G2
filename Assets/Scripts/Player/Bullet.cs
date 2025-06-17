using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effecct = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effecct, 0.3f);
        Destroy(gameObject);
    }
}
