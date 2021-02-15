using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState { START, ORDER_ROLL, ROLL, MOVE, CELL_EVENT, END_TURN, GAMEOVER }

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
    public bool hasRolled = true;
    public string fullText;
    public static int diceNumber;
    public Dictionary<int, int> firstRollResults = new Dictionary<int, int>();
    public Dictionary<int, int> duplDict = new Dictionary<int, int>();
    public GameObject[] boardSpaces = new GameObject[38];
    public int boardSpaceIndex = 0;

    [SerializeField] private GameObject gameBoard;
    [SerializeField] private GameObject middleOfBoard;
    [SerializeField] private Dice dice;
    [SerializeField] private GameObject rollButton;
    [SerializeField] private Player playerOne;
    [SerializeField] private Player playerTwo;
    [SerializeField] private int playerTurn = 100;
    [SerializeField] private new Camera camera;
    [SerializeField] private float delay = 0.03f;
    //[SerializeField] private Dictionary<int, int> firstRollResults = new Dictionary<int, int>();
    //[SerializeField] private Dictionary<int, int> duplDict = new Dictionary<int, int>();

    private Board board;
    private string currentText = "";
    private static int totalPlayers = 2;
    private readonly float textWaitTime = 3f;

    // -- START --
    private void Start()
    {
        state = GameState.START;
        StartCoroutine(SetupScene());
        LeanTween.init(1000);
    }

    // -- UPDATE --
    private void Update()
    {
        if (playerTurn == 0)
        {
            // Enlarge Player 1 HUD, and change its colour

            EnlargeHUDAnimation(playerOneHUD.gameObject);
            playerOneHUD.GetComponent<Image>().color = new Color32(253, 100, 100, 255);

            // Reduce Player 2 HUD
            playerTwoHUD.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            playerTwoHUD.GetComponent<Image>().color = new Color32(254, 255, 196, 255);
        }

        if (playerTurn == 1)
        {
            // Enlarge Player 2 HUD and change its colour
            EnlargeHUDAnimation(playerTwoHUD.gameObject);
            playerTwoHUD.GetComponent<Image>().color = new Color32(253, 100, 100, 255);
            // Reduce Player 1 HUD
            playerOneHUD.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            playerOneHUD.GetComponent<Image>().color = new Color32(254, 255, 196, 255);
        }

        if(hasRolled)
        {
            rollButton.SetActive(false);
        }
        else
            rollButton.SetActive(true);
    }

    private void EnlargeHUDAnimation(GameObject obj)
    {
            LeanTween.scale(obj, new Vector3(1.1f, 1.1f, 1.1f), 0.9f);
            LeanTween.scale(obj, new Vector3(1f, 1f, 1f), 0.9f);
    }

    // -- CO-ROUTINES --
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
        state = GameState.ORDER_ROLL;
        dice.gameObject.SetActive(true);
        rollButton.gameObject.SetActive(true);
        hasRolled = false;
    }

    IEnumerator ProcessOrderRoll()
    {
        // Wait for the dice to throw itself
        yield return new WaitForSeconds(3f);

        // For the last player in beginning roll sequence...
        if (playerTurn == totalPlayers - 1)
        {
            GameObject currentPlayer = GameObject.FindGameObjectWithTag(playerTurn.ToString());

            // Add final player's roll to result of first roll array
            firstRollResults.Add(playerTurn, diceNumber);
            Debug.Log(firstRollResults[playerTurn]);

            // Check for duplicates.

            // IF THERE ARE DUPLICATES, Player should roll again
            if (CheckForDuplicates())
            {
                fullText = "That's another " + diceNumber.ToString() + "!";
                StartCoroutine(TypeText());
                yield return new WaitForSeconds(textWaitTime);

                fullText = currentPlayer.GetComponent<Player>().playerName + " must roll again!";
                StartCoroutine(TypeText());
                firstRollResults.Remove(playerTurn);
                hasRolled = false;
            }
            else // IF NO DUPLICATES, Calculate the game order
            {
                fullText = currentPlayer.GetComponent<Player>().playerName + " has rolled a " + diceNumber.ToString() + ".";
                StartCoroutine(TypeText());

                // Calculate game order

                StartCoroutine(CalculateGameOrder());
                yield return new WaitForSeconds(textWaitTime * 3);

                state = GameState.ROLL;
                hasRolled = false;
            }
        }

        else // For every other player in beginning roll sequence...
        {
            // Get player name and state their roll result
            GameObject currentPlayer = GameObject.FindGameObjectWithTag(playerTurn.ToString());

            // Add final player's roll to result of first roll array
            firstRollResults.Add(playerTurn, diceNumber);
            Debug.Log(firstRollResults[playerTurn]);

            // Check for duplicates
            // IF DUPLICATES Player should roll again
            if(CheckForDuplicates())
            {
                fullText = "That's another " + diceNumber.ToString() + "!";
                StartCoroutine(TypeText());
                yield return new WaitForSeconds(textWaitTime);

                fullText = currentPlayer.GetComponent<Player>().playerName + " must roll again!";
                StartCoroutine(TypeText());
                firstRollResults.Remove(playerTurn);
                hasRolled = false;
            }
            else
            {
                // IF NO DUPLICATES Declare the next players turn

                fullText = currentPlayer.GetComponent<Player>().playerName + " has rolled a " + diceNumber.ToString() + ".";
                StartCoroutine(TypeText());
                yield return new WaitForSeconds(textWaitTime);

                fullText = "It is " + GameObject.FindGameObjectWithTag((playerTurn + 1).ToString()).GetComponent<Player>().playerName + "'s turn to roll...";
                StartCoroutine(TypeText());
                yield return new WaitForSeconds(textWaitTime / 2);

                hasRolled = false;
                NextTurn();
            }
        }
    }

    IEnumerator ProcessRoll()
    {
        // Wait for the dice to throw itself
        yield return new WaitForSeconds(3f);

        GameObject currentPlayer = GameObject.FindGameObjectWithTag(playerTurn.ToString());

        fullText = currentPlayer.GetComponent<Player>().playerName + " has rolled a " + diceNumber.ToString() + ".";
        StartCoroutine(TypeText());

        // Move the player
        StartCoroutine(MoveThePlayer());
    }

    IEnumerator MoveThePlayer()
    {
        yield return new WaitForSeconds(textWaitTime/2);
        GameObject currentPlayer = GameObject.FindGameObjectWithTag(playerTurn.ToString());
        currentPlayer.GetComponent<Player>().boardSpaceIndex += diceNumber;
        currentPlayer.GetComponent<Player>().boardSpaceIndex %= 38;
        currentPlayer.gameObject.GetComponent<Player>().Move(boardSpaces[currentPlayer.GetComponent<Player>().boardSpaceIndex]);
        NextTurn();
        yield return new WaitForSeconds(textWaitTime);

        fullText = "It is " + GameObject.FindGameObjectWithTag((playerTurn).ToString()).GetComponent<Player>().playerName + "'s turn to roll...";
        StartCoroutine(TypeText());
        state = GameState.ROLL;
        hasRolled = false;
    }

    IEnumerator CalculateGameOrder()
    {
        var highestRoll = 0;
        var currentHighestPlayer = 0;

        for(int player = 0; player < totalPlayers; player++)
        {
            if (firstRollResults[player] > highestRoll)
            {
                highestRoll = firstRollResults[player];
                currentHighestPlayer = player;
            }
        }

        yield return new WaitForSeconds(textWaitTime);
        fullText = GameObject.FindGameObjectWithTag(currentHighestPlayer.ToString()).GetComponent<Player>().playerName + " has rolled the highest!";
        StartCoroutine(TypeText());

        yield return new WaitForSeconds(textWaitTime);
        fullText = GameObject.FindGameObjectWithTag(currentHighestPlayer.ToString()).GetComponent<Player>().playerName + " will start the game.";
        StartCoroutine(TypeText());

        playerTurn = currentHighestPlayer;
    }

    public void NextTurn()
    {
        playerTurn += 1;
        playerTurn %= totalPlayers;
    }

    public void OnRoll()
    {
        if(hasRolled == false)
        {
            hasRolled = true;

            if (state == GameState.ORDER_ROLL)
            {
                StartCoroutine(ProcessOrderRoll());
            }
            else if (state == GameState.ROLL)
            {
                StartCoroutine(ProcessRoll());
            }
        }
    }

    private bool CheckForDuplicates()
    {
        // Loop through all the players that have rolled so far
        for(int player = playerTurn; player < (playerTurn + 1); player++)
        {
            // If the roll result value is unique, add it and return false (No Duplicate)
            if (!duplDict.ContainsKey(firstRollResults[playerTurn]))
            {
                duplDict.Add(firstRollResults[playerTurn], 1);
            }
            else
            {
                Debug.Log("DUPLICATE!!!");
                return true;
            }
        }
        Debug.Log("No Duplicates");
        return false;
    }
}