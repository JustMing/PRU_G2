using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void OnSettingSound(float sound)
   {
        Debug.Log("Setting sound to: " + sound);
        GameManager.Instance.SetVolume(sound);
   }

    public void OnExit()
    {
        Debug.Log("Exiting game");
        GameManager.Instance.LoadScene("MainMenu");
    }
}
