using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public static GameManager Instance;
    public float worldSpeed;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerController.Instance.health > 0)
        {
            Pause();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        Debug.Log("Volume set to: " + volume);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetWorldSpeed(float speed)
    {
        worldSpeed = speed;
    }

    public void Pause()
    {
        if(UIController.Instance.pausePanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Detail(GameObject text)
    {
        if (text.activeSelf)
        {
            text.SetActive(false);
        }
        else
        {
            text.SetActive(true);
        }
    }
}
