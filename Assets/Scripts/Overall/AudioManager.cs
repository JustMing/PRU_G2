using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource shipExplosion;
    public AudioSource hit;

    public AudioSource explosionDestroy;
    public AudioSource hitObstacle;
    public AudioSource shoot;
    public AudioSource bg;

    public AudioSource win;

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

    void Start()
    {
        bg.Play();
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }

    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.4f, 0.7f);
        sound.Stop();
        sound.Play();
    }
}
