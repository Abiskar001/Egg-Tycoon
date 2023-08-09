using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedShopManager : MonoBehaviour
{
    Money money;
    [SerializeField] feed feed;
    [SerializeField] int price;
    [SerializeField] Text feedValueText;
    [SerializeField] Text priceText;
    [SerializeField] float feedValue; //value the item adds to the food or water
    Food food;
    Water water;

    private void Awake()
    {
        EventManager.moneyUpdate += colorUpdater;
        money = FindObjectOfType<Money>();
    }

    private void OnDisable()
    {
        EventManager.moneyUpdate -= colorUpdater;
    }
    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price.ToString();
        food = FindObjectOfType<Food>();
        water = FindObjectOfType<Water>();
    }

    public void buyFeed()
    {
        if (money.subtractMoney(price))
        {
            switch (feed)
            {
                case feed.Food:
                    food.foodAmount += feedValue;
                    if (food.foodAmount > food.foodMaxCapacity)
                    {
                        food.foodAmount = food.foodMaxCapacity;
                    }
                    money.money -= price;
                    money.moneyUpdate();
                    break;

                case feed.Water:
                    water.waterAmount += feedValue;
                    if (water.waterAmount > water.waterMaxCapacity)
                    {
                        water.waterAmount = water.waterMaxCapacity;
                    }
                    money.money -= price;
                    money.moneyUpdate();
                    break;
            }
        }
    }
    public void colorUpdater()
    {
        if (money.subtractMoney(price))
        {
            priceText.color = Color.black;
        }
        else
        {
            priceText.color = Color.HSVToRGB(0f, 1f, 0.6f);
        }
    }
}
