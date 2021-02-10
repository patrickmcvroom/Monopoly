using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private int cellPosition;
    [SerializeField] private Board board;

    public string playerName;
    public int cash;
    public int playerID;

    public void Create(Vector3 position)
    {
        Instantiate(transform, position, Quaternion.identity);
    }

    public void Move(int amount)
    {
        cellPosition += amount;
        cellPosition %= Board.CellCount;

        var targetPosition = board.GetCellPosition(cellPosition);

        // animate this
        transform.position = targetPosition;
    }

    //public int Roll(Dice dice)
    //{
    //    return dice.Roll();
    //}

}
