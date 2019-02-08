/* Attach script to any object that the player can push */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable_Object : MonoBehaviour
{
    private bool isPushing = false;
    public GameObject player;
    private Rigidbody rb;

    void start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY
            | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        rb = this.GetComponent<Rigidbody>();
        //print(player.gameObject.GetComponent<PlayerMovement2>().flat);
        if (player.gameObject.GetComponent<PlayerMovement2>().flat == true)
        {
            print("help");
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY
                | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }

    public void push()
    {
        Debug.Log("We is pushing now");

        //InteractText = "Press E to ";

        //isPushing = !isPushing;
        //InteractText += isPushing ? "let go" : "to push";

        //Debug.Log(InteractText);

        // Place animation here https://www.youtube.com/watch?v=1ayGwrHdGVo
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sidescroller"))
        {
            rb.isKinematic = true;
        }
    }
}
