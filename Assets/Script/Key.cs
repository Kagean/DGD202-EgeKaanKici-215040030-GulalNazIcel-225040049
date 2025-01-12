using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isFollowing;
    public float followSpeed;
    public Transform followTarget;
    AudioManager audioManager;
    private AudioSource audioSource;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isFollowing)
            {
                MovementController thePlayer = FindObjectOfType<MovementController>();
                followTarget = thePlayer.KeyFollowPoint;
                isFollowing = true;
                thePlayer.followingKey = this;

                StartCoroutine(PlaySoundAndFollow());
            }
        }
    }

    private IEnumerator PlaySoundAndFollow()
    {
        audioSource.PlayOneShot(audioManager.StarandKey);
        yield return new WaitForSeconds(audioManager.StarandKey.length);
    }
}
