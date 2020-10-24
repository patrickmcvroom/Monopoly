using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Dice dice;
    [SerializeField] private Player currentPlayer;

    private void Start()
    {
        currentPlayer = GameObject.Find("Player1").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            var rollValue = currentPlayer.Roll(dice);

            currentPlayer.Move(rollValue);
        }
    }
}
