using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public GameObject pausePanel;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text expText;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;

    [SerializeField] private GameObject wonGame;

    [SerializeField] private TextMeshProUGUI inGameScoreText;
    private int score;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    public int highScore;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

     void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
            Time.timeScale = 0f;
            if(SceneManager.GetActiveScene().name == "Boss")
            {
                SceneManager.LoadScene("LoseScene");
            } else
            {
                AudioManager.Instance.bg.Stop();
                AudioManager.Instance.PlaySound(AudioManager.Instance.win);
                wonGame.SetActive(true);
            }
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
       
    }

    public void UpdateHealthSlider(float current, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = Mathf.RoundToInt(current);
        healthText.text = healthSlider.value + "/" + healthSlider.maxValue;
    }

    public void UpdateExpSlider(float current, float max)
    {
        expSlider.maxValue = max;
        expSlider.value = Mathf.RoundToInt(current);
        expText.text = expSlider.value + "/" + expSlider.maxValue;
    }

    public void AddPoint(int point)
    {
        score += point;
        inGameScoreText.text = string.Format("{0:0000000}", score);
        highScore = inGameScoreText.text.Length > 0 ? int.Parse(inGameScoreText.text) : 0;
    }
}
