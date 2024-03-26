using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoomerangTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] TextMeshProUGUI lvlUI;

    [Header("Attributes")]
    [SerializeField] private int baseUpgradeCost = 525;

    AudioManager audioManager;

    private int level = 1;

    public GameObject childObject;
    private RotatingBullet childScript;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void Start()
    {
        childScript = childObject.GetComponent<RotatingBullet>();
        upgradeButton.onClick.AddListener(Upgrade);
    }

    public void OpenUpgradeUI()
    {
        if (level < 5)
        { //used for level limits
            upgradeUI.SetActive(true);
        }
        else
        {
            upgradeUI.SetActive(true);
            upgradeButton.gameObject.SetActive(false);
            Debug.Log("max level");
        }
        /*if(level == 1)
        {
            Debug.Log("1");
        }
        else
        {
            Debug.Log("hdjd");
        }*/
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (baseUpgradeCost > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(baseUpgradeCost);

        audioManager.PlaySFX(audioManager.lvlUp);

        level++;

        childScript.SetRotationSpeed(100f);

        lvlUI.text = level.ToString();

        CloseUpgradeUI();
    }
}
