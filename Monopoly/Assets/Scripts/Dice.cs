using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        diceVelocity = rb.velocity;
    }

    public void Throw()
    {
        DiceNumberText.diceNumber = 0;
        float dirX = Random.Range(0, 700);
        float dirY = Random.Range(0, 700);
        float dirZ = Random.Range(0, 700);
        transform.position = new Vector3(0, 2, 0);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 500);
        rb.AddTorque(dirX, dirY, dirZ);
    }
}
