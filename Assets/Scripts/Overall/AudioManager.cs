using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource shipExplosion;
    public AudioSource hit;

    public AudioSource explosionDestroy;
    public AudioSource hitObstacle;
    public AudioSource shoot;

    void Awake()
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

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }

    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.5f, 0.8f);
        sound.Stop();
        sound.Play();
    }
}
