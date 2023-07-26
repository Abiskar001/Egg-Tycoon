using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Egg : MonoBehaviour
{
    private void Start()
    {
        EventManager.eggCollected += PickUpEgg;
    }
    void OnMouseDown()
    {
        // This code will be executed when the egg is clicked
        Destroy(gameObject);
        EventManager.eggPressed();
    }

    void PickUpEgg()
    {
        Debug.Log("Egg picked up!");
        // Add any other code you want to execute when the egg is picked up
    }

    private void OnDisable()
    {
        EventManager.eggCollected -= PickUpEgg;
    }
}
