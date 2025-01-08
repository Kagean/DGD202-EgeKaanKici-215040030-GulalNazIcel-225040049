using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;
    AudioManager audioManager;
    private AudioSource audioSource;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private CollectableStar CollectableStar;

    private void Start()
    {
        CollectableStar = CollectableStar.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            CollectableStar.ChangeStars(value);
            StartCoroutine(PlaySoundAndDestroy());
        }
    }

    private IEnumerator PlaySoundAndDestroy()
    {
        AudioClip starClip = audioManager.StarandKey;
        audioSource.PlayOneShot(starClip);

        // Ses klibinin süresi kadar bekle
        yield return new WaitForSeconds(starClip.length);

        Destroy(gameObject);
    }
}
