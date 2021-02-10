using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour
{
    private Vector3 diceVelocity;

    void FixedUpdate()
    {
        diceVelocity = Dice.diceVelocity;
    }

    void OnTriggerStay(Collider col)
    {
        if(diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch(col.gameObject.name)
            {
                case "Side1":
                    GameManager.diceNumber = 6;
                    break;
                case "Side2":
                    GameManager.diceNumber = 5;
                    break;
                case "Side3":
                    GameManager.diceNumber = 4;
                    break;
                case "Side4":
                    GameManager.diceNumber = 3;
                    break;
                case "Side5":
                    GameManager.diceNumber = 2;
                    break;
                case "Side6":
                    GameManager.diceNumber = 1;
                    break;
            }
        }
    }
}
