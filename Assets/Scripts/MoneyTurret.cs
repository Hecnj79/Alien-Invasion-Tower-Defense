using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] TextMeshProUGUI lvlUI;

    [Header("Attributes")]
    [SerializeField] private int baseUpgradeCost = 800;

    AudioManager audioManager;

    private int level = 1;

    private float timeSeconds = 5f;

    private int increaseMoneyBy = 5;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        StartCoroutine(SpawnTimer());
        upgradeButton.onClick.AddListener(Upgrade);
    }

    private IEnumerator SpawnTimer()
    {

        while (true)
        {
            yield return new WaitForSeconds(timeSeconds);
            LevelManager.main.currency += increaseMoneyBy;
            //Debug.Log(increaseMoneyBy);
            
        }
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

        increaseMoneyBy++;

        lvlUI.text = level.ToString();

        CloseUpgradeUI();
    }
}
