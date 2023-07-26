using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonManager : MonoBehaviour
{
    private GameObject[] shopItems;
    [SerializeField] GameObject designatedItemList;

    private void Awake()
    {
        shopItems = GameObject.FindGameObjectsWithTag("ShopList");
    }
    
    public void turnOnTheList()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].SetActive(false);
        }
        designatedItemList.SetActive(true);
    }

}
