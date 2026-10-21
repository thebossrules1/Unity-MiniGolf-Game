using UnityEngine;

public class GolfBallSFX : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Audio Clips")]
    public AudioClip bounceSound;
    public AudioClip sinkSound;

    void Start()
    {
        // Automatically grab the Audio Source component on this ball
        audioSource = GetComponent<AudioSource>();
    }

    // This triggers automatically when the ball bumps into something
    void OnCollisionEnter(Collision collision)
    {
        // Check if the ball hit a wall (make sure your walls have the "Wall" tag!)
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (bounceSound != null)
            {
                // PlayOneShot allows sounds to overlap smoothly if it bounces quickly
                audioSource.PlayOneShot(bounceSound);
            }
        }
    }

    // Call this function when the ball drops into the hole
    public void PlaySinkSound()
    {
        if (sinkSound != null)
        {
            audioSource.PlayOneShot(sinkSound);
        }
    }
}