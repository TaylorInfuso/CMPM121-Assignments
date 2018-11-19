using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour {

    GameObject character;
    private bool flat;
    private Vector3 originalPos;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        flat = false;
        character = this.gameObject;
        rb = character.GetComponent<Rigidbody>();
	}

    //Set speed of player
    public static float verSpeed = 6.0f;
    public static float horSpeed = 6.0f;

    // Update is called once per frame
    void Update () {
        if (flat == false)
        {
            float straffe = Input.GetAxis("Vertical") * verSpeed; //y input
            float translation = Input.GetAxis("Horizontal") * horSpeed; //x input
            translation *= Time.deltaTime; //calculate distance
            straffe *= Time.deltaTime;
            translation = Mathf.Clamp(translation, translation, translation);
            character.transform.Translate(straffe, 0, -translation);
            if (Input.GetKeyDown("f"))
            {
                originalPos = character.transform.position;
                character.transform.Translate(0, 0, -originalPos.z);
                flat = true;
            }
        } else if(flat == true)
        {
            float translation = Input.GetAxis("Horizontal") * verSpeed; //y input
            translation *= Time.deltaTime; //calculate distance
            translation = Mathf.Clamp(translation, translation, translation);
            character.transform.Translate(translation, 0, 0);
            if (Input.GetKeyDown("f"))
            {
                character.transform.Translate(0, 0, originalPos.z);
                flat = false;
            }
        }
        if (Input.GetKeyDown("space") && rb.velocity.y == 0)
        {
            rb.velocity = new Vector3(0, 10, 0);
        }

    }
}
