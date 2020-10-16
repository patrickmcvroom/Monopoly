using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField]int players = 2;

    // Start is called before the first frame update
    void Start()
    {
        players = 2;
    }

    // Update is called once per frame
    void Update()
    {
        players = Mathf.Clamp(players, 0, 4);
    }
}
