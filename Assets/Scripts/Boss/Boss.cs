using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    private int heath;
    private int maxHealth;
    private Animator animator;
    private float speedX;
    private float speedY;
    private bool isMoving = true;
    private FlashWhite flashWhite;

    private float switchInterval;
    private float switchTimer;
    [SerializeField] private List<Wave> waves;
    [SerializeField] private GameObject spawnPos;
    [SerializeField] private Slider bossSlider;
    [SerializeField] private TMP_Text bossText;

    [System.Serializable]
    public class Wave
    {
        public ObjectPooler pool;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        heath = 500;
        maxHealth = heath;
        animator = GetComponent<Animator>();
        flashWhite = GetComponent<FlashWhite>();
        UpdateHealthSlider(heath, maxHealth);
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
            if (!isMoving)
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

        if (heath <= 0)
        {
            WinGame();
        }
    }

    private void UpdateHealthSlider(float current, float max)
    {
        bossSlider.maxValue = max;
        bossSlider.value = Mathf.RoundToInt(current);
        bossText.text = bossSlider.value + "/" + bossSlider.maxValue;
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
        UpdateHealthSlider(heath, maxHealth);
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
        isMoving = false;
        animator.SetBool("isAttack", false);
        animator.SetBool("isSpawn", false);
    }

    private void SpawnState()
    {
        if(heath < maxHealth /2)
        {
            heath += 20;
            UpdateHealthSlider(heath, maxHealth);
        }
        speedX = 0f;
        speedY = 0f;
        switchInterval = Random.Range(3f, 6f);
        switchTimer = switchInterval;
        int waveNumber = Random.Range(0, waves.Count - 1);
        isMoving = true;
        animator.SetBool("isSpawn", true);
        for (int i = 0; i < switchInterval * 2; i++)
        {
            RandomSpawn(waveNumber);
        }
    }

    private void RandomSpawn(int waveNumber)
    {
        GameObject spawnedObject = waves[waveNumber].pool.GetPooledObject();
        spawnedObject.transform.position = spawnPos.transform.position += new Vector3(Random.Range(-2f, 2f), 0);
        spawnedObject.transform.rotation = transform.rotation;
        spawnedObject.SetActive(true);
    }

    private void WinGame()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.win);
        SceneManager.LoadScene("WinScene");
    }
}
