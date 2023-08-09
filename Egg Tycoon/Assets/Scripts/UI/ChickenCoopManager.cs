using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenCoopManager : MonoBehaviour
{

    public Money money;
    [SerializeField] GameObject[] allChickensInGame;
    [SerializeField] GameObject chicken;
    [SerializeField] Text chickenPrice;
    [SerializeField] Text chickenName;
    [SerializeField] Slider ageSlider;
    [SerializeField] Slider eggSlider;

    public Transform chickenSpawnLocation;

    private void Start()
    {
        EventManager.moneyUpdate += colorUpdater;
        money = GameObject.FindObjectOfType<Money>();
        ageSlider.maxValue = 0;
        eggSlider.maxValue = 0;
        chickenName.text = chicken.name;
        for (int i = 0; i < allChickensInGame.Length; i++)
        {
            if (ageSlider.maxValue < allChickensInGame[i].GetComponent<Chicken>().lifeExpentancy)
            {
                ageSlider.maxValue = allChickensInGame[i].GetComponent<Chicken>().lifeExpentancy;
            }
        }
        for (int i = 0; i < allChickensInGame.Length; i++)
        {
            if (eggSlider.maxValue < allChickensInGame[i].GetComponent<Chicken>().eggLaidPerDay)
            {
                eggSlider.maxValue = allChickensInGame[i].GetComponent<Chicken>().eggLaidPerDay;
            }
        }
        ageSlider.value = chicken.GetComponent<Chicken>().lifeExpentancy;
        eggSlider.value = chicken.GetComponent<Chicken>().eggLaidPerDay;
        chickenPrice.text = chicken.GetComponent<Chicken>().chickenPrice.ToString();
    }

    private void OnDisable()
    {
        EventManager.moneyUpdate -= colorUpdater;
    }

    public void buyChicken()
    {
        if (money.subtractMoney(chicken.GetComponent<Chicken>().chickenPrice))
        {
            money.money -= chicken.GetComponent<Chicken>().chickenPrice;
            var spawnedChicken = Instantiate(chicken);
            spawnedChicken.transform.position = chickenSpawnLocation.position;
            money.moneyUpdate();
        }
    }
    private void Update()
    {
        colorUpdater();
    }
    public void colorUpdater()
    {
        if (money.subtractMoney(chicken.GetComponent<Chicken>().chickenPrice))
        {
            chickenPrice.color = Color.black;
        }
        else
        {
            chickenPrice.color = Color.HSVToRGB(0f, 1f, 0.6f);
        }
    }
}
