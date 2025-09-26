using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // Singleton

    [Header("Audio Sources")]
    private AudioSource audioSource;

    [Header("Clips")]
    public AudioClip backgroundMusic;
    public AudioClip collectSound;
    public AudioClip deathSound;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // sahne deðiþince silinmesin
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    void Start()
    {
        PlayBackground();
    }

    public void PlayBackground()
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayCollect()
    {
        audioSource.PlayOneShot(collectSound);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
