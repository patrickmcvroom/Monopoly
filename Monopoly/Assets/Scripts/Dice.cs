using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;
    [SerializeField] private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        diceVelocity = rb.velocity;
    }

    public void Throw()
    {
        if(game.hasRolled == false)
        {
            DiceNumberText.diceNumber = 0;
            float dirX = Random.Range(0, 1000);
            float dirY = Random.Range(0, 1000);
            float dirZ = Random.Range(0, 1000);
            transform.position = new Vector3(0, 3, 0);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
    }
}
