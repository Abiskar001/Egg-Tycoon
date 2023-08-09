using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEditor;

public class Food : MonoBehaviour,IUpgradeables
{
    public int level = 1;
    public AnimationCurve priceCurve;
    public AnimationCurve capacityCurve;
    public float foodMaxCapacity;
    public float foodAmount;
    public float incrementPerUpgrade;
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
    public void upgradeLevel()
    {
        level += 1;
        foodMaxCapacity += incrementPerUpgrade;
        foodSlider.maxValue = foodMaxCapacity;
    }

    public string currentMax()
    {
        return foodMaxCapacity.ToString();
    }

    public string incrementInUpgrade()
    {
        return incrementPerUpgrade.ToString();
    }

    public int upgradePrice()
    {
        return (int)priceCurve.Evaluate(level);
    }

    [CustomEditor(typeof(Food), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class FoodPriceUpgradeEditor : Editor
    {
        Vector2 scroll;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            Food data = (Food)target;

            scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));

            for (int i = 1; i < 101; i++)
            {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Level " + (i));
                EditorGUILayout.LabelField(((int)data.priceCurve.Evaluate(i)) + " Money");
                EditorGUILayout.LabelField(((int)data.capacityCurve.Evaluate(i)) + " Food");
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndScrollView();
        }
    }
}

