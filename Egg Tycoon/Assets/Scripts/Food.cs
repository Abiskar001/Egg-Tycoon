using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public float foodMaxCapacity;
    public float foodAmount;
    public float hungerIncreaseRate = 2f;
    public Slider foodSlider;

    private void OnValidate()
    {
        foodSlider.maxValue = foodMaxCapacity;
    }

    private void Update()
    {
        foodSlider.value = foodAmount;
    }
}
