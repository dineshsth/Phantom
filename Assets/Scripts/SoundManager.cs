using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip flipAudioClip;
    [SerializeField] private AudioClip matchAudioClip;
    [SerializeField] private AudioClip mismatchAudioClip;
    [SerializeField] private AudioClip gamecompeltedAudioClip;

    private GameManager gameManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.instance;

        gameManager.OnCardFlip += PlayFlipSound;
        gameManager.OnMatch += PlayMatchSound;
        gameManager.OnMismatch += PlayMismatchSound;
        gameManager.OnGameComplete += PlayGameCompletedSound;
    }

    private void OnDisable()
    {
        gameManager.OnCardFlip -= PlayFlipSound;
        gameManager.OnMatch -= PlayMatchSound;
        gameManager.OnMismatch -= PlayMismatchSound;
    }

    private void PlayFlipSound()
    {
        if (flipAudioClip != null)
        {
            audioSource.PlayOneShot(flipAudioClip);
        }
    }

    private void PlayMatchSound()
    {
        if (matchAudioClip != null)
        {
            audioSource.PlayOneShot(matchAudioClip);
        }
    }

    private void PlayMismatchSound()
    {
        if (mismatchAudioClip != null)
        {
            audioSource.PlayOneShot(mismatchAudioClip);
        }
    }
    private void PlayGameCompletedSound()
    {
        if (gamecompeltedAudioClip != null)
        {
            audioSource.PlayOneShot(gamecompeltedAudioClip);
        }
    }
}
