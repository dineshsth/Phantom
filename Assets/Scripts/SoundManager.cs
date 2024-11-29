using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip flipSound;
    public AudioClip matchSound;
    public AudioClip mismatchSound;

    private GameManager gameManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.instance;

        gameManager.OnCardFlip += PlayFlipSound;
        gameManager.OnMatch += PlayMatchSound;
        gameManager.OnMismatch += PlayMismatchSound;
    }

    private void OnDisable()
    {
        gameManager.OnCardFlip -= PlayFlipSound;
        gameManager.OnMatch -= PlayMatchSound;
        gameManager.OnMismatch -= PlayMismatchSound;
    }

    private void PlayFlipSound()
    {
        if (flipSound != null)
        {
            audioSource.PlayOneShot(flipSound);
        }
    }

    private void PlayMatchSound()
    {
        if (matchSound != null)
        {
            audioSource.PlayOneShot(matchSound);
        }
    }

    private void PlayMismatchSound()
    {
        if (mismatchSound != null)
        {
            audioSource.PlayOneShot(mismatchSound);
        }
    }
}
