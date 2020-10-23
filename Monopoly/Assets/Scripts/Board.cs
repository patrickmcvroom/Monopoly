using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[] boardCell = new GameObject[40];

    public int currentRoll;
    int currentSpace = 0;
    int newSpace;



    int Move()
    {
        newSpace = currentSpace + currentRoll;
        return newSpace;
    }

    int Roll()
    {
        currentRoll = Random.Range(1, 12);
        return currentRoll;

        Move();
    }

    // Start is called before the first frame update
    void Start()
    {
        boardCell[0] = GameObject.FindGameObjectWithTag("Go");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Roll();
        }
    }
}
