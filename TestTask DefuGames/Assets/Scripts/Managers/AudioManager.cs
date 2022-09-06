using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource cameraAudioSource;

    [Header("Sounds")]
    public AudioClip rightColorSound;
    public AudioClip wrongColorSound;
    public AudioClip playerDeadSound;
    public AudioClip winSound;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void StopBackgroundMusic()
    {
        cameraAudioSource.Stop();
    }

    public void PlayBackgroundMusic()
    {
        cameraAudioSource.Play();
    }
}
