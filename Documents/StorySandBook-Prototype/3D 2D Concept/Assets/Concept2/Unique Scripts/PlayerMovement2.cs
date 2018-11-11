using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*ATTACHED TO PLAYER PREFAB
 * In editor, you may specify whether player will begin flat
 */

public class PlayerMovement2 : MonoBehaviour {

   
    GameObject character;
    Rigidbody rb;
    CapsuleCollider col;
    AudioSource audioSource;
    [SerializeField] AudioClip doorOpenClip;
    [SerializeField] AudioClip pillarHitClip;
    [SerializeField] ParticleSystem boom;
    public float rotationAngle = 0;
    public bool flat = false;
    private bool door = false;

    //Set speed of player
    public static float verSpeed = 6.0f;
    public static float horSpeed = 6.0f;

    //Checkpoint Position
    public Vector3 checkpos;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKey("q"))
        {
            rotationAngle -= 0.05f;
        }
        if (Input.GetKey("e"))
        {
            rotationAngle += 0.05f;
        }
        if (flat == false)
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
                audioSource.PlayOneShot(doorOpenClip);
                Invoke("ToFlat", 0.75f);// Time gap
                rotationAngle = 0;
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
                if (checkpos != new Vector3(0, 0, 0))
                character.transform.position = checkpos;
                print(checkpos);
            }
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.Euler(0, rotationAngle / Mathf.PI * 180, 0), Time.deltaTime * 10.0f);
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
                audioSource.PlayOneShot(doorOpenClip);
                StartCoroutine(FromFlat(temp));// Time gap
            }
            if ((Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("space")) && bottom2D == true)
            {
                temp.z = 42;
                rb.velocity = temp;
                bottom2D = false;
            }
            if (Input.GetKeyDown("r"))
            {
                if (checkpos != new Vector3(0, 0, 0))
                    character.transform.position = checkpos;
                print(checkpos);
            }
        }
    }

    private void ToFlat()
    {
        flat = true;
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
				if (other.gameObject.CompareTag("Pick Up"))
			other.gameObject.SetActive(false);
    }

    private bool bottom2D = false;
    private bool bottom3D = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            audioSource.PlayOneShot(pillarHitClip);
        }
        if (collision.gameObject.CompareTag("GameOver"))
        {
          //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.CompareTag("Tree"))
        {
            boom.Play();
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
}
