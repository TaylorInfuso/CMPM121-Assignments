using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ATTACHED TO PLAYER PREFAB
 * In editor, you may specify whether player will begin flat
 */

public class PlayerMovement2 : MonoBehaviour {

    GameObject character;
    public bool flat = false;
    Rigidbody rb;
    CapsuleCollider col;
    private bool door = false;

    //Set speed of player
    public static float verSpeed = 6.0f;
    public static float horSpeed = 6.0f;

    // Use this for initialization
    void Start () {

        character = this.gameObject;

        rb = character.GetComponent<Rigidbody>();
        col = character.GetComponentInChildren<CapsuleCollider>();
        if (flat == true)
        {
            col.radius = 0;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            this.gameObject.transform.Rotate(90f, 0, 0);
            Vector3 scale = this.transform.Find("Body").localScale;
            scale.z = 1 / 181f;
            this.transform.Find("Body").localScale = scale;
        }
    }

    void Update () {
        if(flat == false)
        {
            
            float straffe = Input.GetAxis("Vertical") * verSpeed; //y input
            float translation = Input.GetAxis("Horizontal") * horSpeed; //x input
            translation *= Time.deltaTime; //calculate distance
            straffe *= Time.deltaTime;
            translation = Mathf.Clamp(translation, translation, translation);
            character.transform.Translate(straffe, 0, -translation);

            if(character.transform.eulerAngles.x != 0)
            {
                this.gameObject.transform.Rotate(-90f, 0, 0);
                col.radius = 0.5f; //Change radius here to avoid player clipping
                Vector3 scale = this.transform.Find("Body").localScale;
                scale.z = 1f;
                this.transform.Find("Body").localScale = scale;
            }
            if (Input.GetKeyDown("f") && door == true)
            {
                flat = true;
                col.radius = 0;
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }
            if (Input.GetKeyDown("space") && bottom3D == true)
            {
                Vector3 temp = rb.velocity;
                temp.y = 10;
                rb.velocity = temp;
                bottom3D = false;
            }
        }
        else if (flat == true)
        {
            float translation = Input.GetAxis("Horizontal") * verSpeed; //y input
            translation *= Time.deltaTime; //calculate distance
            translation = Mathf.Clamp(translation, translation, translation);
            character.transform.Translate(translation, 0, 0);
            Vector3 temp = rb.velocity;
            temp.z -= 0.7f;
            rb.velocity = temp;
            if (character.transform.eulerAngles.x != 90)
            {
                this.gameObject.transform.Rotate(90f, 0, 0);
                //character.GetComponent<BoxCollider>().enabled = true; //enable here to avoid player clipping
                Vector3 scale = this.transform.Find("Body").localScale;
                scale.z = 1 / 181f;
                this.transform.Find("Body").localScale = scale;
            }
            if (Input.GetKeyDown("f") && door == true)
            {
                flat = false;
                flat = false;
                temp.z = 0;
                rb.velocity = temp;
                //character.GetComponent<BoxCollider>().enabled = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
            if ((Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("space")) && bottom2D == true)
            {
                temp.z = 30;
                rb.velocity = temp;
                bottom2D = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DoorScript>() != null)
        {
            door = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DoorScript>() != null)
        {
            door = false;
        }
    }

    private bool bottom2D = false;
    private bool bottom3D = false;
    private void OnCollisionEnter(Collision collision)
    {
        //fake raycasting
        foreach (ContactPoint contact in collision)
        {
            Debug.Log(contact.point.z);
            if (contact.point.z < gameObject.transform.position.z + 0.1)
                bottom2D = true;
            if (contact.point.y < gameObject.transform.position.y + 0.1)
                bottom3D = true;
        }
    }
}
