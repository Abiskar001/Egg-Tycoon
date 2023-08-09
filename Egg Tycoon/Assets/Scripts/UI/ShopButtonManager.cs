using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ShopButtonManager : MonoBehaviour
{
    public GameObject[] shopItems;
    [SerializeField] GameObject designatedItemList;

    
    public void turnOnTheList()
    {
        //turn all shoplist off
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].SetActive(false);
        }
        designatedItemList.SetActive(true);
        EventManager.moneyyUpdate();
    }

    [System.Obsolete]
    public void activeFlipFlop()
    {
        if (designatedItemList.active)
        {
            designatedItemList.SetActive(false);
        }
        else
        {
            designatedItemList.SetActive(true);
        }
    }
}


