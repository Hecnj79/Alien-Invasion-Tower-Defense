using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObj;
    public Turret turret;
    public TurretSlowMo turretSlowMo;
    public BoomerangTurret turretRotating;
    public MoneyTurret moneyTurret;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (towerObj != null)
        {
            if (turret)
            {
                turret.OpenUpgradeUI();
                return;
            }
            else
            {
                if (turretSlowMo)
                {
                    turretSlowMo.OpenUpgradeUI();
                    return;
                }
                else
                {
                    if (turretRotating)
                    {
                        turretRotating.OpenUpgradeUI();
                        return;
                    }
                    else
                    {
                        if (moneyTurret)
                        {
                            moneyTurret.OpenUpgradeUI();
                            return;
                        }
                    }
                }
            }
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if(towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("Cant afford");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
        turretSlowMo = towerObj.GetComponent<TurretSlowMo>();
        turretRotating = towerObj.GetComponent<BoomerangTurret>();
        moneyTurret = towerObj.GetComponent<MoneyTurret>();
    }
}
