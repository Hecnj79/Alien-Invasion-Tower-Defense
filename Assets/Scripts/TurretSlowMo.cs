using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class TurretSlowMo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] TextMeshProUGUI lvlUI;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; //attacks per second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private int baseUpgradeCost = 700;

    AudioManager audioManager;

    private float apsBase;
    private float targetingRangeBase;

    private float timeUntilFire;

    private int level = 1;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        apsBase = aps;
        targetingRangeBase = targetingRange;

        upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if(timeUntilFire >= 1f / aps)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
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

        aps += 0.25f;
        targetingRange += 0.25f;

        lvlUI.text = level.ToString();

        CloseUpgradeUI();
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
