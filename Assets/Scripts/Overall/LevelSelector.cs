using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }
    public void ChooseLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
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
