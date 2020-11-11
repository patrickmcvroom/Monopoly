using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int CellCount = 40;
    private int firstRowIndex = 0;
    private int secondRowIndex = 9;
    private int thirdRowIndex = 18;
    private int fourthRowIndex = 27;

    [SerializeField] private Cell cornerCell;
    [SerializeField] private Cell sideCell;
    [SerializeField] private Texture[] textures = new Texture[CellCount];
    [SerializeField] private TileConfiguration[] cornerConfigs = new TileConfiguration[CellCount/10];
    [SerializeField] private TileConfiguration[] sideConfigs = new TileConfiguration[35];
    [SerializeField] private Cell[] cells = new Cell[CellCount];
    

    private void InstantiateFirstRow()
    {
        // INSTANTIATE FIRST ROW

        for (int corner = 0; corner < 1; corner++)
        {
            var cellZValue = 6.38f;

            cells[corner] = Instantiate(cornerCell, new Vector3(6.38f, 0.05f, cellZValue), Quaternion.identity);
            cells[corner].transform.SetParent(transform);
            cells[corner].SetConfiguration(cornerConfigs[0]);
            cells[corner].transform.Rotate(0f, 270f, 0f, Space.World);
            cellZValue -= Cell.CornerSize.width / 2 + Cell.SideSize.width / 2;

            for (int side = 1; side < 10; side++)
            {
                cells[side] = Instantiate(sideCell, new Vector3(6.38f, 0.05f, cellZValue), Quaternion.identity);
                cells[side].transform.SetParent(transform);
                cells[side].SetConfiguration(sideConfigs[firstRowIndex]);
                cellZValue -= Cell.SideSize.width;
                firstRowIndex++;
            }
        }
    }

    private void InstantiateSecondRow()
    {
        // INSTANTIATE SECOND ROW

        for (int corner = 10; corner < 11; corner++)
        {
            var cellXValue = 6.38f;

            cells[corner] = Instantiate(cornerCell, new Vector3(cellXValue, 0.05f, -6.4f), Quaternion.identity);
            cells[corner].transform.Rotate(0f, -90f, 0, Space.World);
            cells[corner].transform.SetParent(transform);
            cells[corner].SetConfiguration(cornerConfigs[1]);
            cellXValue -= Cell.CornerSize.width / 2 + Cell.SideSize.width / 2;

            for (int side = 11; side < 20; side++)
            {
                cells[side] = Instantiate(sideCell, new Vector3(cellXValue, 0.05f, -6.4f), Quaternion.identity);
                cells[side].transform.SetParent(transform);
                cells[side].transform.Rotate(0f, 90f, 0f, Space.World);
                cells[side].SetConfiguration(sideConfigs[secondRowIndex]);
                cellXValue -= Cell.SideSize.width;
                secondRowIndex++;
            }
        }
    }

    private void InstantiateThirdRow()
    {
        // INSTANTIATE THIRD ROW

        for (int corner = 20; corner < 21; corner++)
        {
            var cellZValue = -6.4f;

            cells[corner] = Instantiate(cornerCell, new Vector3(-6.4f, 0.05f, cellZValue), Quaternion.identity);
            cells[corner].transform.SetParent(transform);
            cells[corner].transform.Rotate(0f, -90f, 0f, Space.World);
            cells[corner].SetConfiguration(cornerConfigs[2]);
            cellZValue += Cell.CornerSize.width / 2 + Cell.SideSize.width / 2;

            for (int side = 21; side < 30; side++)
            {
                cells[side] = Instantiate(sideCell, new Vector3(-6.4f, 0.05f, cellZValue), Quaternion.identity);
                cells[side].transform.SetParent(transform);
                cells[side].transform.Rotate(0f, 180f, 0f, Space.World);
                cells[side].SetConfiguration(sideConfigs[thirdRowIndex]);
                cellZValue += Cell.SideSize.width;
                thirdRowIndex++;
            }
        }
    }

    private void InstantiateFourthRow()
    {
        // INSTANTIATE FOURTH ROW

        for (int corner = 30; corner < 31; corner++)
        {
            var cellXValue = -6.4f;

            cells[corner] = Instantiate(cornerCell, new Vector3(cellXValue, 0.05f, 6.4f), Quaternion.identity);
            cells[corner].transform.SetParent(transform);
            cells[corner].transform.Rotate(0f, -90f, 0f, Space.World);
            cells[corner].SetConfiguration(cornerConfigs[3]);
            cellXValue += Cell.CornerSize.width / 2 + Cell.SideSize.width / 2;

            for (int side = 31; side < 40; side++)
            {
                cells[side] = Instantiate(sideCell, new Vector3(cellXValue, 0.05f, 6.4f), Quaternion.identity);
                cells[side].transform.Rotate(0f, 270f, 0f, Space.World);
                cells[side].SetConfiguration(sideConfigs[fourthRowIndex]);
                cells[side].transform.SetParent(transform);
                cellXValue += Cell.SideSize.width;
                fourthRowIndex++;
            }
        }
    }

    public void Create(float width, float height, Vector3 position)
    {
        var child = transform.GetChild(0);

        var scale = child.lossyScale;

        scale.x = width;
        scale.z = height;

        child.localScale = scale;

        cornerConfigs = Resources.LoadAll<TileConfiguration>("Corner Tile Configs");
        sideConfigs = Resources.LoadAll<TileConfiguration>("Side Tile Configs");

        InstantiateFirstRow();
        InstantiateSecondRow();
        InstantiateThirdRow();
        InstantiateFourthRow();
    }

    public float GetBoardWidth()
    {
        return (Cell.CornerSize.width * 2) + (Cell.SideSize.width * 9);
    }

    public Vector3 GetCellPosition(int index)
    {
        return cells[index].transform.position;
    }
}
