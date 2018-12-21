using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*ATTACHED TO PLAYER PREFAB
 * In editor, you may specify whether player will begin flat
 */

public class PlayerMovement2 : MonoBehaviour
{


    GameObject character;
    Rigidbody rb;
    CapsuleCollider col;
    BoxCollider box;
    AudioSource audioSource;
    [SerializeField] AudioClip doorOpenClip;
    [SerializeField] AudioClip pillarHitClip;
    [SerializeField] ParticleSystem boom;

    public float rotationLeftAndRight = 0;
    private float mouseX;
    public bool flat = false;
    private bool door = false;


    //Set speed of player
    public static float verSpeed = 6.0f;
    public static float horSpeed = 6.0f;

    //Checkpoint Position
    public Vector3 checkpos;

    // Use this for initialization
    void Start()
    {
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
            this.gameObject.transform.Rotate(90f, 0, 0);
            Vector3 scale = this.transform.Find("Body").localScale;
            scale.z = 1 / 181f;
            this.transform.Find("Body").localScale = scale;
        }
    }

    void FixedUpdate()
    {
        if (GlobalVariables.doorNum <= 0)
        {
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
            if (flat == false)
            {

                float straffe = Input.GetAxis("Vertical") * verSpeed; //y input
                float translation = Input.GetAxis("Horizontal") * horSpeed; //x input
                translation *= Time.deltaTime; //calculate distance
                straffe *= Time.deltaTime;
                translation = Mathf.Clamp(translation, translation, translation);
                character.transform.Translate(straffe, 0, -translation);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, rotationLeftAndRight / Mathf.PI * 180 + Mathf.PI / 2, 0), Time.deltaTime * 10.0f);
                if (character.transform.eulerAngles.x != 0)
                {
                    //this.gameObject.transform.Rotate(-90f, 0, 0);
                    col.radius = 0.5f; //Change radius here to avoid player clipping
                    Vector3 scale = this.transform.Find("Body").localScale;
                    scale.z = 1f;
                    this.transform.Find("Body").localScale = scale;
                }
                if (Input.GetKeyDown("f") && door == true)
                {
                    box.enabled = flat;
                    col.enabled = !flat;
                    audioSource.PlayOneShot(doorOpenClip);
                    Invoke("ToFlat", 0.75f);// Time gap
                    rotationLeftAndRight = 0;
                }
                if (Input.GetKeyDown("space") && bottom3D == true)
                {
                    Vector3 temp = rb.velocity;
                    temp.y = 10;
                    rb.velocity = temp;
                    bottom3D = false;
                }
                if (Input.GetKeyDown("r"))
                {
                    int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currenSceneIndex);
                }
                //character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.Euler(0, rotationAngle / Mathf.PI * 180, 0), Time.deltaTime * 10.0f);
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
                    box.enabled = flat;
                    col.enabled = !flat;
                    audioSource.PlayOneShot(doorOpenClip);
                    StartCoroutine(FromFlat(temp));// Time gap
                }
                if ((Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("space")) && bottom2D == true)
                {
                    temp.z = 35;
                    rb.velocity = temp;
                    bottom2D = false;
                }
                if (Input.GetKeyDown("r"))
                {
                    GlobalVariables.doorNum = 1;
                    int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currenSceneIndex);
                }
            }
        }
    }

    private void ToFlat()
    {
        flat = true;
        box.enabled = flat;
        col.enabled = !flat;
        col.radius = 0;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    // The gap between the door sound effect and actual vision switch
    IEnumerator FromFlat(Vector3 temp)
    {
        yield return new WaitForSeconds(0.75f);
        flat = false;
        temp.z = 0;
        rb.velocity = temp;
        //character.GetComponent<BoxCollider>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
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
        if (collision.gameObject.CompareTag("Object"))
        {
            audioSource.PlayOneShot(pillarHitClip);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            Invoke("LoadNextScene", 2f);
        }
        if (collision.gameObject.CompareTag("Pick Up"))
        {
            collision.gameObject.SetActive(false);
        }

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
    private void LoadNextScene()
    {
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currenSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}