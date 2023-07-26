using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField] private float upTime;
    private float currentUpTime;
    private Chicken[] chickens;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        chickens = FindObjectsOfType<Chicken>();
        currentUpTime += Time.deltaTime;
        if (currentUpTime > upTime)
        {
            for (int i = 0; i < chickens.Length; i++)
            {
                chickens[i].poopOnGround = true;
                chickens[i].health -= Time.deltaTime;
            }
        }
    }

    void OnMouseDown()
    {
        // This code will be executed when the poop is clicked
        chickens = FindObjectsOfType<Chicken>();
        for (int i = 0; i < chickens.Length; i++)
        {
            chickens[i].poopOnGround = false;
        }
        Destroy(gameObject);
    }
}
