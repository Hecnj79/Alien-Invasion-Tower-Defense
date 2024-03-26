using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public GameObject gameOverMenu;

    public Transform startPoint;
    public Transform[] path;

    public float health;
    public int currency;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        health = 100;
        currency = 650;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        currency = Mathf.Clamp(currency, 0, 100000);
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= currency)
        {
            currency -= amount;
            //currency = Mathf.Clamp(currency, 0, 2000);
            return true;
        }
        else
        {
            Debug.Log("not enough money");
            return false;
        }
    }
}
