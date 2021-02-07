using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { START, PLAYER_ORDER, ROLL, MOVE, CELL_EVENT, END_TURN, GAMEOVER }

public class GameManager : MonoBehaviour
{
    public GameState state;

    [SerializeField] private GameObject gameBoard;
    [SerializeField] private Dice dice;
    [SerializeField] private Player playerOne;
    [SerializeField] private int playerTurn;
    [SerializeField] private new Camera camera;

    private Board board;
     
    private void Start()
    {
        state = GameState.START;
        SetupScene();
    }

    void SetupScene()
    {
        // Set the camera position and rotation
        camera.transform.position = new Vector3(7.58f, 9.4f, 0f);
        camera.transform.eulerAngles = new Vector3(60, 270, 0);

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
