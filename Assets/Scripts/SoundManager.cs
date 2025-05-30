using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }
}
