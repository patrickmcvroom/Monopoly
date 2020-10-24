using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int CellCount = 40;

    [SerializeField] private Cell cornerCell;
    [SerializeField] private Cell sideCell;
    [SerializeField] private Cell[] cells = new Cell[CellCount];


    public Vector3 GetCellPosition(int index)
    {
        return cells[index].transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        var boardWidth = (Cell.CornerSize.width * 2) + (Cell.SideSize.width * 9);
        var boardHeight = boardWidth;

        var child = transform.GetChild(0);

        var scale = child.lossyScale;

        scale.x = boardHeight;
        scale.z = boardWidth;

        child.localScale = scale;
    }
}
