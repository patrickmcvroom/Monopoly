using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]int cash;
    [SerializeField]bool isBankrupt;
    [SerializeField]int position;


    void Start()
    {
        cash = 1500;
        isBankrupt = false;
    }

    // Update is called once per frame
    void Update()
    {
        cash = Mathf.Clamp(cash, 0, 50000);
    }
}
