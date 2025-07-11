using UnityEditorInternal;
using UnityEngine;

public class ShipWeapon : Weapon
{
    public static ShipWeapon Instance;

    //[SerializeField] private GameObject prefab;
    [SerializeField] private GameObject bulletPosition;
    [SerializeField] private ObjectPooler bulletPool;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance  = this;  
        }
    }

    public void Shoot()
    {
        //Instantiate(prefab, bulletPosition.transform.position, bulletPosition.transform.rotation);
        //GameObject bullet = bulletPool.GetPooledObject();
        //bullet.transform.position = bulletPosition.transform.position;
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.shoot);
        for (int i = 0; i < stats[weaponLevel].amount; i++)
        {
            GameObject bullet = bulletPool.GetPooledObject();
            float xPos = bulletPosition.transform.position.x;

            if (stats[weaponLevel].amount > 1)
            {
                float spacing = stats[weaponLevel].range / (stats[weaponLevel].amount - 1);
                xPos = bulletPosition.transform.position.x - (stats[weaponLevel].range / 2) + i * spacing;
            }

            bullet.transform.position = new Vector2(xPos, bulletPosition.transform.position.y);
            bullet.transform.localScale = new Vector2(stats[weaponLevel].size, stats[weaponLevel].size);
            bullet.SetActive(true);
        }
    }

    public void LevelUp()
    {
        if(weaponLevel <  stats.Count - 1)
        {
            weaponLevel++;
        }
    }
}
