using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject playButton;

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI healthUI;
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] Animator anim;

    AudioManager audioManager;

    private bool isMenuOpen = true;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ToggleMenu()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    public void HomeButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Destroy(audioManager.gameObject);
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        pauseButton.SetActive(false);
        playButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        pauseButton.SetActive(true);
        playButton.SetActive(false);
        Time.timeScale = 1f;
    }

    public void TryAgainButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        healthUI.text = LevelManager.main.health.ToString();
        waveUI.text = EnemySpawner.main.currentWave.ToString();
    }

    public void SetSelected()
    {

    }
}
