using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager1 : MonoBehaviour
{
    public GameObject shopUI;

    void turnShopOn()
    {
        shopUI.SetActive(true);
    }
}
