using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*ATTACHED TO PLAYER PREFAB
 * In editor, you may specify whether player will begin flat
 */

public class newPlayerMovement2 : MonoBehaviour
{
    GameObject character;
    public Rigidbody rb;
    CapsuleCollider col;
    BoxCollider box;
    AudioSource audioSource;
    [SerializeField] AudioClip doorOpenClip;
    [SerializeField] AudioClip pillarHitClip;
    [SerializeField] ParticleSystem boom;

    private float mouseX;
    public bool flat = false;
    private bool door = false;

    //Set speed of player
    public float verSpeed = 6.0f;
    public float horSpeed = 6.0f;

    private int jumpFC; //frame count that jump has been activated

    //Checkpoint Position
    public Vector3 checkpos;

    //this variable is so tht the player matches the rotation of the door they enter
    private Vector3 doorAngle;
    private Vector3 properDoorAngle;
    private Vector3 flatForce;

    //this variable exists to only shift the character's rotation a single time
    private bool shouldShift = false;

    //for sticking to wall
    private float stickVar;

    //Rotation
    public float rotationLeftAndRight;

    // Use this for initialization
    void Start()
    {
        //move this code later
        jumpFC = 0;
        print(this.transform.eulerAngles.y);
        print(this.transform.eulerAngles.y / 180);

        rotationLeftAndRight = this.transform.eulerAngles.y/180;

        audioSource = GetComponent<AudioSource>();
        character = this.gameObject;
        rb = character.GetComponent<Rigidbody>();
        col = character.GetComponentInChildren<CapsuleCollider>();
        box = character.GetComponentInChildren<BoxCollider>();
        box.enabled = flat;
        if (flat == true)
        {
            col.radius = 0;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            this.gameObject.transform.Rotate(0, 0, 0);
            Vector3 scale = this.transform.Find("Body").localScale;
            scale.z = 1 / 181f;
            this.transform.Find("Body").localScale = scale;
        }
    }

    void FixedUpdate()
    {
        print(this.transform.eulerAngles.y);
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > mouseX)
            {
                rotationLeftAndRight += 0.065f;
            }
            else if (Input.mousePosition.x < mouseX)
            {
                rotationLeftAndRight -= 0.065f;
            }
        }

        mouseX = Input.mousePosition.x;

        if (Input.GetKey("left"))
        {
            rotationLeftAndRight -= 0.05f;
        }

        if (Input.GetKey("right"))
        {
            rotationLeftAndRight += 0.05f;
        }

        //functions for if the player is 3D
        if (flat == false)
        {

            Vector3 temp = rb.velocity;

            float straffe = Input.GetAxis("Vertical") * verSpeed; //y input
            float translation = Input.GetAxis("Horizontal") * horSpeed; //x input
            translation *= Time.deltaTime; //calculate distance
            straffe *= Time.deltaTime;
            translation = Mathf.Clamp(translation, translation, translation);
            character.transform.Translate(straffe, 0, -translation);

            print(rotationLeftAndRight);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(transform.eulerAngles.x, rotationLeftAndRight * 180, 0), Time.deltaTime * 10.0f);

            col.radius = 0.5f; //Change radius here to avoid player clipping
            Vector3 scale = this.transform.Find("Body").localScale;
            scale.z = 1f;
            this.transform.Find("Body").localScale = scale;

            //enter door
            if (Input.GetKeyDown("f") && door == true)
            {
                box.enabled = flat;
                col.enabled = !flat;
                audioSource.PlayOneShot(doorOpenClip);
                Invoke("ToFlat", 0.75f);// Time gap

                //Invoke("ToFlat", 0.15f);// Time gap
                rotationLeftAndRight = 0;
            }

            //jump
            if (Input.GetKeyDown("space") && bottom3D == true && jumpFC == 0)
            {
                temp.y = 8;
                rb.velocity = temp;
                bottom3D = false;
            }
            else if (Input.GetKey("space") && jumpFC <= 20)
            {
                temp.y = 8;
                rb.velocity = temp;
                jumpFC++;
            }
            else
            {
                jumpFC = 21;
            }
            if (bottom3D == true && rb.velocity.y == 0)
            {
                jumpFC = 0;
            }
        }

        //functions for if the player is 2D
        else if (flat == true)
        {
            float translation = Input.GetAxis("Horizontal") * verSpeed; //y input

            Vector3 stickRot = character.GetComponent<Transform>().localEulerAngles;

            //Add constraints in movements
            if (doorAngle[0] != 0)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                stickVar = stickRot[1];
                while (stickVar > 1 || stickVar < -1)
                    stickVar = stickVar / 180;
            }
            else
            {
                stickVar = 0;
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }

            translation *= Time.deltaTime; //calculate distance
            translation = Mathf.Clamp(translation, translation, translation);
            //character.transform.Translate(straffe, 0, -translation);
            character.transform.Translate(translation, 0, stickVar);

            //this is the new code for gravity in a 2d space. The force on the player is now dependent on the angle of the door
            Vector3 temp = rb.velocity;

            if (doorAngle[0] == 0)
            {
                temp.z -= 0.7f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
                temp.x -= 0.7f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
            }
            else
            {
                temp.y -= 0.7f;
            }

            flatForce = temp;
            rb.velocity = temp;

            //player angle
            if (shouldShift == true && doorAngle[0] == 0)
            {
                this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(doorAngle[0] + 90f, doorAngle[1], doorAngle[2]));
            }

            if (shouldShift == true && doorAngle[0] != 0)
            {
                this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(doorAngle[0] + 90f, doorAngle[1], doorAngle[2] - 180f));
            }

            //player scale
            Vector3 scale = this.transform.Find("Body").localScale;
            scale.z = 1 / 181f;
            this.transform.Find("Body").localScale = scale;
            shouldShift = false;

            //Enter door
            if (Input.GetKeyDown("f") && door == true)
            {
                box.enabled = flat;
                col.enabled = !flat;
                audioSource.PlayOneShot(doorOpenClip);
                StartCoroutine(FromFlat(temp));// Time gap
            }

            //Jump
            if ((Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("space")) && bottom2D == true)
            {
                //this is gravity change
                if (doorAngle[0] == 0)
                {
                    temp.z = 30 * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
                    temp.x = 30 * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
                }
                else
                {
                    temp.y = 30;
                }

                rb.velocity = temp;
                bottom2D = false;
            }
        }

        //reset
        if (Input.GetKeyDown("r"))
        {
            int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currenSceneIndex);
        }
    }



    private void ToFlat()
    {
        flat = true;
        shouldShift = true;
        box.enabled = flat;
        col.enabled = !flat;
        col.radius = 0;
    }

    // The gap between the door sound effect and actual vision switch
    IEnumerator FromFlat(Vector3 temp)
    {
        shouldShift = false;
        yield return new WaitForSeconds(0.05f);
        flat = false;
        temp.z = 0;
        rb.velocity = temp;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DoorScript>() != null)
        {
            door = true;
            if (flat == false)
            {
                doorAngle = other.gameObject.GetComponent<Transform>().localEulerAngles;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DoorScript>() != null)
        {
            door = false;
        }

    }

    public bool bottom2D = false;
    public bool bottom3D = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            audioSource.PlayOneShot(pillarHitClip);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            Invoke("LoadNextScene", 2f);
        }


        if (collision.gameObject.CompareTag("Sidescroller"))
        {
            if (flat == true)
            {
                Vector3 newRot = collision.gameObject.GetComponent<Transform>().localEulerAngles;
                newRot[1] = newRot[1] - 90;

                character.GetComponent<Transform>().localEulerAngles = newRot;
                Vector3 stickRot = character.GetComponent<Transform>().localEulerAngles;
                stickVar = newRot[1];
                while (stickVar > 1 || stickVar < -1)
                    stickVar = stickVar / 90;
            }
        }


        if (collision.gameObject.CompareTag("Pick Up"))
        {
            //there is now a qualifier to make sure you are on the same dimension when collecting the pickup
            if((collision.gameObject.GetComponent<Rotater>().flat == true && flat == true) || (collision.gameObject.GetComponent<Rotater>().flat == false && flat == false))
            collision.gameObject.SetActive(false);
        }

        //fake raycasting
        foreach (ContactPoint contact in collision)
        {
            //Debug.Log(contact.point.z);
            if (doorAngle[0] == 0)
            {
                //Calculate collision based on the angle of player
                if (contact.point.z < gameObject.transform.position.z - 0.7f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI))
                {
                    bottom2D = true;
                }

                if (contact.point.x < gameObject.transform.position.x - 0.7f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI))
                {
                    bottom2D = true;
                }
            }
            else if (doorAngle[0] > 0)
            {
                if (contact.point.y < gameObject.transform.position.y + 1)
                    bottom2D = true;
            }
            if (contact.point.y < gameObject.transform.position.y + 0.1)
                bottom3D = true;
        }

    }

    private void LoadNextScene()
    {
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currenSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}