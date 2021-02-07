using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { START, PONEROLL, PONEMOVE, PONECARD, PONEDONE, PTWOROLL, PTWOMOVE, PTWOCARD, PTWODONE, GAMEOVER }

public class GameManager : MonoBehaviour
{
    public GameState state;

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
        state = GameState.START;

        SetupScene();
    }

    void SetupScene()
    {
        // Create instance of the board
        var gameObject = Instantiate(gameBoard, Vector3.zero, Quaternion.identity);
        board = gameObject.GetComponent<Board>();

        // Generate the board
        board.Create(board.GetBoardWidth(), board.GetBoardWidth(), Vector3.zero);

        // Generate the players
        //playerOne.Create(board.GetCellPosition(15));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
                playerOne.Create(board.GetCellPosition(0));
        }

        if (Input.GetKeyDown("r"))
        {
            var rollValue = playerOne.Roll(dice);

            playerOne.Move(rollValue);
        }




    }

    IEnumerator TwoSeconds()
    {
        yield return new WaitForSeconds(2f);
    }
}
