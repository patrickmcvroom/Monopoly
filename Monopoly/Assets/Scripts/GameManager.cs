using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState { START, PLAYER_ORDER, ROLL, MOVE, CELL_EVENT, END_TURN, GAMEOVER }

public class GameManager : MonoBehaviour
{
    public GameState state;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI playerOneNameText;
    public TextMeshProUGUI playerOneCashText;
    public TextMeshProUGUI playerTwoNameText;
    public TextMeshProUGUI playerTwoCashText;
    public string fullText;
    public static int diceNumber;

    [SerializeField] private GameObject gameBoard;
    [SerializeField] private GameObject middleOfBoard;
    [SerializeField] private Dice dice;
    [SerializeField] private GameObject rollButton;
    [SerializeField] private Player playerOne;
    [SerializeField] private Player playerTwo;
    [SerializeField] private int playerTurn;
    [SerializeField] private new Camera camera;
    [SerializeField] private float delay = 0.03f;

    private Board board;
    private string currentText = "";
    private int totalPlayers = 2;

    // Called at initialization
    private void Start()
    {
        state = GameState.START;
        StartCoroutine(SetupScene());
    }

    private void Update()
    {
        switch(state)
        {
            case GameState.PLAYER_ORDER:
                //Show dice and roll button
                dice.gameObject.SetActive(true);
                rollButton.SetActive(true);

                // Set player turn
                playerTurn = 1;
                // Clamp the player turn variable
                playerTurn %= totalPlayers;

                break;
            case GameState.ROLL:
                //Show dice and roll button
                dice.gameObject.SetActive(true);
                rollButton.SetActive(true);

                // If pl

                break;
            case GameState.MOVE:
                //Hide dice and roll button
                dice.gameObject.SetActive(false);
                rollButton.SetActive(false);
                //Hide the dialogue panel
                dialoguePanel.SetActive(false);
                break;
            case GameState.CELL_EVENT:
                //Show the dialogue panel
                dialoguePanel.SetActive(true);
                break;
            case GameState.END_TURN:
                //Hide the dialogue panel
                dialoguePanel.SetActive(false);
                break;
            case GameState.GAMEOVER:
                //Hide dice and roll button
                dice.gameObject.SetActive(false);
                rollButton.SetActive(false);
                break;
        }
    }

    IEnumerator SetupScene()
    {
        // Deactivate the dice and roll button
        dice.gameObject.SetActive(false);
        rollButton.SetActive(false);

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
        playerOne.Create(new Vector3(board.GetCellPosition(0).x + 0.4f, board.GetCellPosition(0).y, board.GetCellPosition(0).z));
        playerTwo.Create(new Vector3(board.GetCellPosition(0).x - 0.4f, board.GetCellPosition(0).y, board.GetCellPosition(0).z));

        // Set Player Units
        playerOne.cash = 1500;
        playerTwo.cash = 1500;

        playerOneNameText.text = playerOne.playerName;
        playerOneCashText.text = "£" + playerOne.cash.ToString();
        playerTwoNameText.text = playerTwo.playerName;
        playerTwoCashText.text = "£" + playerTwo.cash.ToString();

        // Setup the dialogue
        dialoguePanel.SetActive(true);
        fullText = "Welcome to Monopoly!";
        StartCoroutine(TypeText());
        yield return new WaitForSeconds(4f);
        fullText = "Which player will go first?";
        StartCoroutine(PlayerOrderTypeText());
        yield return new WaitForSeconds(1.5f);
        //PlayerOrderTypeText function will call a function to switch state to PLAYER_ORDER
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length+1; i++)
        {
            currentText = fullText.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator PlayerOrderTypeText()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        state = GameState.PLAYER_ORDER;
    }
}
