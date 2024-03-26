using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip BGMusic;
    public AudioClip enemyDeath;
    public AudioClip gameOver;
    public AudioClip lvlUp;
    public AudioClip gameWin;
    public AudioClip buttonClick;
    public AudioClip minusHealth;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        musicSource.clip = BGMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
