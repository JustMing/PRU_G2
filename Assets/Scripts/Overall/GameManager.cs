using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject pauseMenuPrefab;
    private GameObject pauseMenuInstance;
    public static bool IsPause = false;
    public static GameManager Instance;
    public float worldSpeed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        TogglePause();
    }
    private void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != ("MainMenu"))
        {
            if (IsPause)
            {
                GameResume();
            }
            else
            {
                GamePause();
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        Debug.Log("Volume set to: " + volume);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void GamePause()
    {
        Time.timeScale = 0f;
        IsPause = true;
        Debug.Log("Game Paused");
        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = Instantiate(pauseMenuPrefab);
        }
        pauseMenuInstance.SetActive(true);
    }

    private void GameResume()
    {
        Time.timeScale = 1f;
        IsPause = false;
        Debug.Log("Game Resumed");
        if (pauseMenuInstance != null)
        {
            pauseMenuInstance.SetActive(false);
        }
    }
}
