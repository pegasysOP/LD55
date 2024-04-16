using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton Stuff
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField] AudioSource audioSource;

    [Header("Clips")]
    [SerializeField] AudioClip gameWinSound;

    public void PlayGameFinishSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameWinSound);
    }
}
