using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int CellCount = 40;
    [SerializeField] private GameObject[] cells = new GameObject[CellCount];

    public Vector3 GetCellPosition(int index)
    {
        return cells[index].transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

   
}
