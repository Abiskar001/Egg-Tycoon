using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int money;
    public Text moneyText;

    private void Awake()
    {
        moneyUpdate();
    }
    public void addMoney(int amount)
    {
        money += amount;
        moneyUpdate();
    }

    public bool subtractMoney(int amount)
    {
        if (amount <= money)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Updates money text ingame
    public void moneyUpdate()
    {
        moneyText.text = money.ToString();
        EventManager.moneyyUpdate();
    }
}
