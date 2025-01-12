using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
    AudioManager audioManager;
    private AudioSource audioSource;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlaySoundAndLoadScene());
        }
    }

    private IEnumerator PlaySoundAndLoadScene()
    {
        audioSource.PlayOneShot(audioManager.Finish);
        yield return new WaitForSeconds(audioManager.Finish.length);

        if (goNextLevel)
        {
            SceneController.instance.NextLevel();
        }
        else
        {
            SceneController.instance.LoadScene(levelName);
        }
    }
}
