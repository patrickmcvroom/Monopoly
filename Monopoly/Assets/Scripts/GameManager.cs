using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState { START, PLAYER_ONE_ROLL, PLAYER_TWO_ROLL, ROLL, MOVE, CELL_EVENT, END_TURN, GAMEOVER }

public class GameManager : MonoBehaviour
{
    public GameState state;
    public GameObject playerOneHUD;
    public GameObject playerTwoHUD;
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
    [SerializeField] private int playerTurn = 100;
    [SerializeField] private new Camera camera;
    [SerializeField] private float delay = 0.03f;
    [SerializeField] private bool hasRolled = false;

    private Board board;
    private string currentText = "";
    private readonly int totalPlayers = 2;
    private int playerOneFirstRoll;
    private int playerTwoFirstRoll;

    // Called at initialization
    private void Start()
    {
        state = GameState.START;
        StartCoroutine(SetupScene());
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

        // Set Player HUDs and Units
        playerOneHUD.GetComponent<Image>().color = new Color32(254, 255, 196, 255);
        playerTwoHUD.GetComponent<Image>().color = new Color32(254, 255, 196, 255);
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
        for (int i = 0; i < fullText.Length + 1; i++)
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
        playerTurn = 0;
        state = GameState.PLAYER_ONE_ROLL;
        dice.gameObject.SetActive(true);
        rollButton.gameObject.SetActive(true);
    }

    public void NextTurn()
    {
        playerTurn += 1;
        playerTurn %= totalPlayers;
    }

    IEnumerator ProcessPlayerOneRoll()
    {
        yield return new WaitForSeconds(3f);

        GameObject currentPlayer = GameObject.FindGameObjectWithTag(playerTurn.ToString());
        

        fullText = currentPlayer.GetComponent<Player>().playerName + " has rolled a " + diceNumber.ToString() + ".";
        StartCoroutine(TypeText());
        hasRolled = false;
        NextTurn();
    }

    IEnumerator ProcessPlayerTwoRoll()
    {
        yield return new WaitForSeconds(3f);
        fullText = playerTwo.playerName + " has roled a " + diceNumber.ToString() + ".";
        StartCoroutine(TypeText());
        hasRolled = false;
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void OnRoll()
    {
        hasRolled = true;

        if(state == GameState.PLAYER_ONE_ROLL)
        {
            StartCoroutine(ProcessPlayerOneRoll());
        }
        else if(state == GameState.ROLL)
        {
            GameObject.FindGameObjectWithTag(playerTurn.ToString());
        }
    }

    // ----- UPDATE FUNCTION -----
    private void Update()
    {
        if(playerTurn == 0)
        {
            // Enlarge Player 1 HUD, and change its colour
            playerOneHUD.gameObject.transform.localScale = new Vector3(1.14f, 1.14f, 1.14f);
            playerOneHUD.GetComponent<Image>().color = new Color32(253, 100, 100, 255);
            // Reduce Player 2 HUD
            playerTwoHUD.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            playerTwoHUD.GetComponent<Image>().color = new Color32(254, 255, 196, 255);
        }

        if(playerTurn == 1)
        {
            // Enlarge Player 2 HUD and change its colour
            playerTwoHUD.gameObject.transform.localScale = new Vector3(1.14f, 1.14f, 1.14f);
            playerTwoHUD.GetComponent<Image>().color = new Color32(253, 100, 100, 255);
            // Reduce Player 1 HUD
            playerOneHUD.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            playerOneHUD.GetComponent<Image>().color = new Color32(254, 255, 196, 255);
        }
    }
}
