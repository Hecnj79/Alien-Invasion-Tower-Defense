using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject InfoScreen;
    public GameObject turretInfoScreen;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadScene(1);
    }

    public void InfoButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        InfoScreen.SetActive(true);
    }

    public void TurretInfoButton()
    {
        //InfoScreen.SetActive(false);
        audioManager.PlaySFX(audioManager.buttonClick);
        turretInfoScreen.SetActive(true);
    }

    public void CloseInfoButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        InfoScreen.SetActive(false);
        turretInfoScreen.SetActive(false);
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Application.Quit();
    }
}
