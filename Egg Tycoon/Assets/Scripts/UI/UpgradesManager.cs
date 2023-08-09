using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUpgradeables
{
    string currentMax();
    string incrementInUpgrade();
    void upgradeLevel();
    int upgradePrice();
}
public class UpgradesManager : MonoBehaviour
{
    public Money money;
    [SerializeField] GameObject item; //The item to upgrade
    [SerializeField] Text priceText, upgradeInfoText;

    private void OnDisable()
    {
        EventManager.moneyUpdate -= colorUpdater;
    }
    private void Awake()
    {
        money = GameObject.FindObjectOfType<Money>();
        EventManager.moneyUpdate += colorUpdater;
    }

    //updates upgrade info and price text in shop
    public void textUpdater()
    {
        upgradeInfoText.text = item.GetComponent<IUpgradeables>().currentMax() + " + " + item.GetComponent<IUpgradeables>().incrementInUpgrade();
        priceText.text = item.GetComponent<IUpgradeables>().upgradePrice().ToString();
    }

    private void Update()
    {
        textUpdater();
    }

    public void upgradeItem()
    {
        if (money.subtractMoney(item.GetComponent<IUpgradeables>().upgradePrice()))
        {
            money.money -= item.GetComponent<IUpgradeables>().upgradePrice();
            money.moneyUpdate();
            item.GetComponent<IUpgradeables>().upgradeLevel();
        }
    }

    public void colorUpdater()
    {
        if (money.subtractMoney(item.GetComponent<IUpgradeables>().upgradePrice()))
        {
            priceText.color = Color.black;
        }
        else
        {
            priceText.color = Color.HSVToRGB(0f, 1f, 0.6f);
        }
    }
}
