using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChooseLevel(string name)
    {
        GameManager.Instance.LoadScene(name);
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
