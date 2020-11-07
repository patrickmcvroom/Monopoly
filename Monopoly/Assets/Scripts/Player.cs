using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int cash;
    [SerializeField] bool isBankrupt;
    [SerializeField] int position;
    [SerializeField] Board board;

    public void Move(int amount)
    {
        position += amount;
        position %= Board.CellCount;

        var targetPosition = board.GetCellPosition(position);

        // animate this
        transform.position = targetPosition;
    }

    public int Roll(Dice dice)
    {
        return dice.Roll();
    }

    void Start()
    {
        cash = 1500;
        isBankrupt = false;
        
        position = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cash = Mathf.Clamp(cash, 0, 50000);
    }
}
