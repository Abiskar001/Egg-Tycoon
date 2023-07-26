using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    public float waterMaxCapacity;
    public float waterAmount;
    public float thirstIncreaseRate = 2f;
    public Slider waterSlider;

    private void OnValidate()
    {
        waterSlider.maxValue = waterMaxCapacity;
    }

    private void Update()
    {
        waterSlider.value = waterAmount;
    }
}
