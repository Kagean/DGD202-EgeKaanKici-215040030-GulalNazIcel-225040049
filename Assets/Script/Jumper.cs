using UnityEngine;

public class Jumper : MonoBehaviour
{
    private float bounce = 17f;
    AudioManager audioManager;
    private AudioSource audioSource;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            PlayJumpSound();
        }
    }

    private void PlayJumpSound()
    {
        audioSource.PlayOneShot(audioManager.Jump);
    }
}
