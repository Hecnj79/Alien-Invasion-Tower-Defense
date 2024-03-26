using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void LevelOne()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Destroy(audioManager.gameObject);
        SceneManager.LoadScene(2);
    }

    public void LevelTwo()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Destroy(audioManager.gameObject);
        SceneManager.LoadScene(3);
    }

    public void LevelThree()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Destroy(audioManager.gameObject);
        SceneManager.LoadScene(4);
    }

    public void BackButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadScene(0);
    }
}
