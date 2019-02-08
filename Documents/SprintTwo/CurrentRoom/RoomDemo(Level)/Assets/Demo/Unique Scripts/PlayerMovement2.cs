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
	public GameObject camera;
    public Rigidbody rb;
    CapsuleCollider col;
    BoxCollider box;
    AudioSource audioSource;
    [SerializeField] AudioClip doorOpenClip;
    [SerializeField] AudioClip pillarHitClip;
    [SerializeField] ParticleSystem boom;

	//GameObjects to interact with
	public GameObject carpet;
    public GameObject blanket;


    //These are for calculating what to do when a puzzle commences
    public GameObject puzzleBlock;
    public Vector3 puzzleAngle;

    public float rotationLeftAndRight;
    private float mouseX;
    public bool flat = false;
    private bool door = false;


    //Set speed of player
    //public static float verSpeed = 6.0f;
    //public static float horSpeed = 6.0f;
    public float verSpeed = 6.0f;
    public float horSpeed = 6.0f;
	public float jumpVar2d = 1;

    private int jumpFC; //frame count that jump has been activated
    private int secondJumpFC;

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

    //for pickup objects
    private int pickUpCounts;

    // Use this for initialization
    void Start()
    {
        //move this code later
        jumpFC = 0;

        audioSource = GetComponent<AudioSource>();
        character = this.gameObject;
        rb = character.GetComponent<Rigidbody>();
        col = character.GetComponentInChildren<CapsuleCollider>();
        box = character.GetComponentInChildren<BoxCollider>();
        box.enabled = flat;

        rotationLeftAndRight = this.transform.localEulerAngles.y;

        pickUpCounts = 0;
        /*
        if (flat == true)
        {
            col.radius = 0;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            this.gameObject.transform.Rotate(90f, 0, 0);
            Vector3 scale = this.transform.Find("Body").localScale;
            scale.z = 1 / 181f;
            this.transform.Find("Body").localScale = scale;
        }
        */
    }

    void FixedUpdate()
    {
        if (GlobalVariables.doorNum <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > mouseX)
                {
                    rotationLeftAndRight += 1.1f;
                }
                else if (Input.mousePosition.x < mouseX)
                {
                    rotationLeftAndRight -= 1.1f;
                }
            }

            mouseX = Input.mousePosition.x;

            if (Input.GetKey("left"))
            {
                rotationLeftAndRight -= 1f;
            }
            if (Input.GetKey("right"))
            {
                rotationLeftAndRight += 1f;
            }
            if (flat == false)
            {
                Vector3 temp = rb.velocity;

                float straffe = Input.GetAxis("Vertical") * verSpeed; //y input
                float translation = Input.GetAxis("Horizontal") * horSpeed; //x input
                translation *= Time.deltaTime; //calculate distance
                straffe *= Time.deltaTime;
                translation = Mathf.Clamp(translation, translation, translation);
                character.transform.Translate(straffe, 0, -translation);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, rotationLeftAndRight, 0), Time.deltaTime * 10.0f);

                col.radius = 0.5f; //Change radius here to avoid player clipping
                Vector3 scale = this.transform.Find("Body").localScale;
                scale.z = 1f;
                this.transform.Find("Body").localScale = scale;

                if (Input.GetKeyDown("f") && door == true)
                {
                    box.enabled = flat;
                    col.enabled = !flat;
                    audioSource.PlayOneShot(doorOpenClip);
                    Invoke("ToFlat", 0.75f);// Time gap

                    //Invoke("ToFlat", 0.15f);// Time gap
                    rotationLeftAndRight = 0;
                }
				
				//Jump
                if (Input.GetKey("space") && bottom3D == true && jumpFC == 0)
                {
                    temp.y += 7f;
                    rb.velocity = temp;
                    bottom3D = false;
                }
				else if (Input.GetKey("space") &&  jumpFC <= 17)
                {
                    temp.y += 0.3f;
					jumpFC++;
                    rb.velocity = temp;
                }
				else if (Input.GetKey("space") &&  jumpFC <= 24)
                {
 
                    temp.y += 0.1f;
					jumpFC++;
                    rb.velocity = temp;
                }
				else if (!Input.GetKey("w") && !Input.GetKey("up") && !Input.GetKey("space"))
                {
                    jumpFC = 25;
                }
                if (bottom3D == true && rb.velocity.y == 0)
                {
                    jumpFC = 0;
                }
                if (Input.GetKeyDown("r"))
                {
                    int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currenSceneIndex);
                }
                //character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.Euler(0, rotationAngle / Mathf.PI * 180, 0), Time.deltaTime * 10.0f);
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
                    //while (stickVar > 1 || stickVar < -1)
                    //    stickVar = stickVar / 180;
                }
                else
                {
                    //stickVar = 0;
                    rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                }

                translation *= Time.deltaTime; //calculate distance
                translation = Mathf.Clamp(translation, translation, translation);
                //character.transform.Translate(straffe, 0, -translation);
                character.transform.Translate(translation, 0, 0);

                //this is the new code for gravity in a 2d space. The force on the player is now dependent on the angle of the door
                Vector3 temp = rb.velocity;

                if (doorAngle[0] == 0)
                {
                    if(doorAngle[1] == 0)
                        temp.z += 0.7f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
                    if(doorAngle[1] == 90)
                        temp.x -= 0.7f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
                    if (doorAngle[1] == 180)
                        temp.z += 0.7f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
                    if (doorAngle[1] == 270)
                        temp.x += 0.7f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
                }
                else
                {
                    temp.y -= 0.7f;
                }
                //temp.x = 0;
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
                if (Input.GetKey("space") && bottom2D == true && secondJumpFC == 0)
                {
                    //this is gravity change originally was 30 *
                    if (doorAngle[0] == 0)
                    {						
						if(doorAngle[1] == 0){
							temp.z -= 20f;
						}
						if(doorAngle[1] == 90){
							temp.z += 20f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
							temp.x += 20f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
						}
						if(doorAngle[1] == 180){
							temp.z += 20f;
						}
						if(doorAngle[1] == 270){
							temp.z -= 20f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
							temp.x -= 20f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
						}

                    }
                    else
                    {
                        temp.y += 20f;
                    }
                    rb.velocity = temp;
                    bottom2D = false;
                }
				else if (Input.GetKey("space") &&  secondJumpFC <= 17)
                {
                    //this is gravity change originally was 30 *
                    if (doorAngle[0] == 0)
                    {						
						if(doorAngle[1] == 0){
							temp.z -= 0.5f;
						}
						if(doorAngle[1] == 90){
							temp.z += 0.5f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
							temp.x += 0.5f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
						}
						if(doorAngle[1] == 180){
							temp.z += 0.5f;
						}
						if(doorAngle[1] == 270){
							temp.z -= 0.5f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
							temp.x -= 0.5f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
						}
					secondJumpFC++;
                    rb.velocity = temp;
					}
                }
				else if (Input.GetKey("space") &&  secondJumpFC <= 24)
                {
                    //this is gravity change originally was 30 *
                    if (doorAngle[0] == 0)
                    {						
						if(doorAngle[1] == 0){
							temp.z -= 0.2f;
						}
						if(doorAngle[1] == 90){
							//temp.z += 0.2f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
							temp.x += 0.2f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
						}
						if(doorAngle[1] == 180){
							temp.z += 0.2f;
						}
						if(doorAngle[1] == 270){
							//temp.z -= 0.2f * (float)Math.Cos(doorAngle[1] / 180 * Math.PI);
							temp.x -= 0.2f * (float)Math.Sin(doorAngle[1] / 180 * Math.PI);
						}
					secondJumpFC++;
                    rb.velocity = temp;
					}
                }
				else if (!Input.GetKey("w") && !Input.GetKey("up") && !Input.GetKey("space"))
                {
                    secondJumpFC = 25;
                }
				if(doorAngle[1] == 0 || doorAngle[1] == 180){ 
					if (bottom2D == true)
					{
						secondJumpFC = 0;
						jumpVar2d = 1;
					}
				}
				print(temp.z);
            }
        }
    }
        void ToFlat(){
        flat = true;
        shouldShift = true;
        box.enabled = flat;
        col.enabled = !flat;
        col.radius = 0;
		Vector3 pos = this.gameObject.transform.position;
		pos[1] += 0.01f;
		this.gameObject.transform.position = pos;
        //rb.constraints = RigidbodyConstraints.FreezeRotation;


        Color color = blanket.GetComponent<Renderer>().material.color;
        color.a = 0.2f;
        blanket.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    // The gap between the door sound effect and actual vision switch
    IEnumerator FromFlat(Vector3 temp)
    {
        //yield return new WaitForSeconds(0.75f);

        shouldShift = false;
        yield return new WaitForSeconds(0.05f);
        flat = false;
        temp.x = 0;
        temp.z = 0;
        rb.velocity = temp;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
		
		//camera.transform.localEulerAngles = this.transform.localEulerAngles;

        Color color = blanket.GetComponent<Renderer>().material.color;
        color.a = 1f;
        blanket.GetComponent<Renderer>().material.SetColor("_Color", color);
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
            //Vector3 doorRotation = other.gameObject.rotation;
        }

        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            pickUpCounts += 1;
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
                Vector3 temp = rb.velocity;
                temp.x = 0;
                rb.velocity = temp;

                while (stickVar > 1 || stickVar < -1)
                    stickVar = stickVar / 90;
            }
        }

        if (collision.gameObject.CompareTag("pushable"))
        {
            Debug.Log("Press E to push");
            //if (Input.GetButton("Push"))
            if (Input.GetKey("e"))
            {
                collision.gameObject.GetComponent<Pushable_Object>().push();
            }
        }

        if (collision.gameObject.CompareTag("puzzle"))
        {
            puzzleAngle = collision.transform.localEulerAngles;
            GlobalVariables.doorNum = 2;
        }


        if (collision.gameObject.CompareTag("Pick Up"))
        {
            //there is now a qualifier to make sure you are on the same dimension when collecting the pickup
            if ((collision.gameObject.GetComponent<Rotater>().flat == true && flat == true) || (collision.gameObject.GetComponent<Rotater>().flat == false && flat == false))
                collision.gameObject.SetActive(false);
        }

        //fake raycasting
        foreach (ContactPoint contact in collision)
        {
            //Debug.Log(contact.point.z);
            if (doorAngle[0] == 0)
            {
                if (doorAngle[1] >= 0 && doorAngle[1] < 90 && contact.point.z < gameObject.transform.position.z + 0.1)
                    bottom2D = true;
                else if (doorAngle[1] >= 90 && doorAngle[1] < 180 && contact.point.x < gameObject.transform.position.x + 0.1)
                    bottom2D = true;
                else if (doorAngle[1] >= 180 && doorAngle[1] < 270 && contact.point.z < gameObject.transform.position.z + 0.1)
                    bottom2D = true;
                else if (doorAngle[1] >= 270 && doorAngle[1] < 360 && contact.point.x < gameObject.transform.position.x + 0.1)
                    bottom2D = true;
            }
            else if (doorAngle[0] > 0)
            {
                if (contact.point.y < gameObject.transform.position.y + 0.1)
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