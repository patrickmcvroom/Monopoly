using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int CellCount = 40;

    [SerializeField] private Cell cornerCell;
    [SerializeField] private Cell sideCell;
    [SerializeField] private Texture[] textures = new Texture[CellCount];
    [SerializeField] private TileConfiguration[] configs = new TileConfiguration[CellCount];
    [SerializeField] private Cell[] cells = new Cell[CellCount];
    
    //[SerializeField] private Vector3 cellPos;
    
    private void InstantiateFirstRow()
    {
        // INSTANTIATE FIRST ROW

        for (int corner = 0; corner < 1; corner++)
        {
            var cellZValue = 6.38f;

            cells[corner] = Instantiate(cornerCell, new Vector3(6.38f, 0.05f, cellZValue), Quaternion.identity);
            cells[corner].transform.SetParent(transform);
            cells[corner].SetConfiguration(configs[corner]);
            cells[corner].transform.Rotate(0f, 270f, 0f, Space.World);
            cellZValue -= Cell.CornerSize.width / 2 + Cell.SideSize.width / 2;

            for (int side = 0; side < 9; side++)
            {
                cells[side] = Instantiate(sideCell, new Vector3(6.38f, 0.05f, cellZValue), Quaternion.identity);
                cells[side].transform.SetParent(transform);
                cells[side].SetTexture(textures[side]);
                cellZValue -= Cell.SideSize.width;
            }
        }
    }

    private void InstantiateSecondRow()
    {
        // INSTANTIATE SECOND ROW

        for (int corner = 0; corner < 1; corner++)
        {
            var cellXValue = 6.38f;

            cells[corner] = Instantiate(cornerCell, new Vector3(cellXValue, 0.05f, -6.4f), Quaternion.identity);
            cells[corner].transform.SetParent(transform);
            cellXValue -= Cell.CornerSize.width / 2 + Cell.SideSize.width / 2;

            for (int side = 0; side < 9; side++)
            {
                cells[side] = Instantiate(sideCell, new Vector3(cellXValue, 0.05f, -6.4f), Quaternion.identity);
                cells[side].transform.SetParent(transform);
                cells[side].transform.Rotate(0f, 90f, 0f, Space.World);
                cellXValue -= Cell.SideSize.width;
            }
        }
    }

    private void InstantiateThirdRow()
    {
        // INSTANTIATE THIRD ROW

        for (int corner = 0; corner < 1; corner++)
        {
            var cellZValue = -6.4f;

            cells[corner] = Instantiate(cornerCell, new Vector3(-6.4f, 0.05f, cellZValue), Quaternion.identity);
            cells[corner].transform.SetParent(transform);
            cellZValue += Cell.CornerSize.width / 2 + Cell.SideSize.width / 2;

            for (int side = 0; side < 9; side++)
            {
                cells[side] = Instantiate(sideCell, new Vector3(-6.4f, 0.05f, cellZValue), Quaternion.identity);
                cells[side].transform.SetParent(transform);
                cells[side].transform.Rotate(0f, 180f, 0f, Space.World);
                cellZValue += Cell.SideSize.width;
            }
        }
    }

    private void InstantiateFourthRow()
    {
        // INSTANTIATE FOURTH ROW

        for (int corner = 0; corner < 1; corner++)
        {
            var cellXValue = -6.4f;

            cells[corner] = Instantiate(cornerCell, new Vector3(cellXValue, 0.05f, 6.4f), Quaternion.identity);
            cells[corner].transform.SetParent(transform);
            cellXValue += Cell.CornerSize.width / 2 + Cell.SideSize.width / 2;

            for (int side = 0; side < 9; side++)
            {
                cells[side] = Instantiate(sideCell, new Vector3(cellXValue, 0.05f, 6.4f), Quaternion.identity);
                cells[side].transform.Rotate(0f, 270f, 0f, Space.World);
                cells[side].transform.SetParent(transform);
                cellXValue += Cell.SideSize.width;
            }
        }
    }

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

        configs = Resources.LoadAll<TileConfiguration>("Tile Configs");

        InstantiateFirstRow();
        InstantiateSecondRow();
        InstantiateThirdRow();
        InstantiateFourthRow();


    }
}
