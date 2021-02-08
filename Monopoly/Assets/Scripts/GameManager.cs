using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState { START, PLAYER_ORDER, ROLL, MOVE, CELL_EVENT, END_TURN, GAMEOVER }

public class GameManager : MonoBehaviour
{
    public GameState State;
    public TextMeshProUGUI DialogueText;
    public string fullText;

    [SerializeField] private GameObject gameBoard;
    [SerializeField] private GameObject middleOfBoard;
    [SerializeField] private Dice dice;
    [SerializeField] private Player playerOne;
    [SerializeField] private int playerTurn;
    [SerializeField] private new Camera camera;
    [SerializeField] private float delay = 0.03f;

    private Board board;
    private string currentText = "";

    // Called at initialization
    private void Start()
    {
        State = GameState.START;
        StartCoroutine(SetupScene());
    }

    IEnumerator SetupScene()
    {
        // Set the camera position and rotation
        camera.transform.position = new Vector3(7.58f, 9.4f, 0f);
        camera.transform.eulerAngles = new Vector3(60, 270, 0);

        // Create instance of the board
        var gameObject = Instantiate(gameBoard, Vector3.zero, Quaternion.identity);
        board = gameObject.GetComponent<Board>();

        // Generate the board
        board.Create(board.GetBoardWidth(), board.GetBoardWidth(), Vector3.zero);
        Instantiate(middleOfBoard, new Vector3(0f, 0.01f, 0f), Quaternion.identity);

        // Generate the player

        // Setup the dialogue
        fullText = "Welcome to Monopoly!";
        StartCoroutine(TypeText());
        yield return new WaitForSeconds(4f);
        fullText = "Which player will go first?";
        StartCoroutine(TypeText());
        State = GameState.PLAYER_ORDER;
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length+1; i++)
        {
            currentText = fullText.Substring(0, i);
            DialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
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
