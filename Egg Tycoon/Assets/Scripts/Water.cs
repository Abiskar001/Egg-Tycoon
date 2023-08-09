using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Water : MonoBehaviour, IUpgradeables
{
    public int level = 1;
    public AnimationCurve priceCurve;
    public AnimationCurve capacityCurve;
    public float waterMaxCapacity;
    public float waterAmount;
    public float incrementPerUpgrade;
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
    public void upgradeLevel()
    {
        level += 1 ;
        waterMaxCapacity += incrementPerUpgrade;
        waterSlider.maxValue = waterMaxCapacity;
    }

    public string currentMax()
    {
        return waterMaxCapacity.ToString();
    }

    public string incrementInUpgrade()
    {
        return incrementPerUpgrade.ToString();
    }

    public int upgradePrice()
    {
        return (int)priceCurve.Evaluate(level);
    }

    [CustomEditor(typeof(Water), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class WaterPriceUpgradeEditor : Editor
    {
        Vector2 scroll;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            Water data = (Water)target;

            scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));

            for (int i = 1; i < 101; i++)
            {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Level " + (i));
                EditorGUILayout.LabelField(((int)data.priceCurve.Evaluate(i)) + " Money");
                EditorGUILayout.LabelField(((int)data.capacityCurve.Evaluate(i)) + " Water");
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndScrollView();
        }
    }
}
