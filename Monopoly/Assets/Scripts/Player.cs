using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int cash;
    [SerializeField] private int cellPosition;
    [SerializeField] private Board board;

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

    void Start()
    {
        cash = 1500;
        cellPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cash = Mathf.Clamp(cash, 0, 50000);

    }
}
