using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private Dice dice;
    [SerializeField] private Player playerOne;
    //[SerializeField] private Player playerTwo;
    //[SerializeField] private Player playerThree;
    //[SerializeField] private Player playerFour;
    //[SerializeField] private Player currentPlayer;
    private Board board;

    private void Start()
    {
        // Create instance of the board
        var gameObject = Instantiate(gameBoard, Vector3.zero, Quaternion.identity);
        board = gameObject.GetComponent<Board>();

        // Generate the board
        board.Create(board.GetBoardWidth(), board.GetBoardWidth(), Vector3.zero);

        Debug.Log("Cell 15 position is " + board.GetCellPosition(15));

        // Generate the players
        //playerOne.Create(board.GetCellPosition(15));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            if (board.GetCellPosition(15) == null)
            {
                Debug.Log("NULL");
            }
            else
            {
                Debug.Log("NOT NULL");
                playerOne.Create(board.GetCellPosition(10));
            }
        }

        //if (Input.GetKeyDown("space"))
        //{
        //    var rollValue = currentPlayer.Roll(dice);

        //    currentPlayer.Move(rollValue);
        //}

        


    }

    IEnumerator TwoSeconds()
    {
        yield return new WaitForSeconds(2f);
    }
}
