using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private int cellWidth;
    [SerializeField] private int cellHeight;

    // Start is called before the first frame update
    void Start()
    {
        var scale = gameObject.GetComponentInChildren<Transform>().localScale;
        cellWidth = (int)scale.z;
        cellWidth = (int)scale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
