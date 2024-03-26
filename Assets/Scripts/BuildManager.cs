using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;

    AudioManager audioManager;

    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public Tower GetSelectedTower()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        selectedTower = _selectedTower;
    }
}
