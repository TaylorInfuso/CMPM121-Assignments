using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraObject : MonoBehaviour
{
	
	public GameObject player;
	GameObject currentHit;
	public GameObject puzzleBlock;
	Vector3 angle = new Vector3();
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(GlobalVariables.doorNum > 0){
			print("you got it dude");
			Vector3 loc = puzzleBlock.GetComponent<PuzzleCube>().puzzleAngle;
			print(loc[1]);
			this.GetComponent<Transform>().localEulerAngles = loc;
            //this.GetComponent<Transform>().rotation = Quaternion.Euler(puzzleBlock.GetComponent<PuzzleCube>().puzzleAngle);
            this.GetComponent<Transform>().localPosition = puzzleBlock.GetComponent<PuzzleCube>().puzzleLoc;
            //Vector3 loc = transform.position;
            //loc[1] = 30;
            //transform.position = loc;
            if (Input.GetKey("left"))
            {
                transform.Translate(-Vector3.right * 20.0f * Time.deltaTime);
            }

            if (Input.GetKey("right"))
            {
                transform.Translate(Vector3.right * 20.0f * Time.deltaTime);
            }
            if (Input.GetKey("up"))
            {
                transform.Translate(Vector3.up * 20.0f * Time.deltaTime);
            }
            if (Input.GetKey("down"))
            {
                transform.Translate(-Vector3.up * 20.0f * Time.deltaTime);
            }

            if (Input.GetKey("["))
            {
                GlobalVariables.doorNum--;
            }

        }
		    else if(player.GetComponent<PlayerMovement2>().flat == true) //if player is 2d
            {
				                Vector3 newPos = player.transform.position;
                Vector3 newRot = player.GetComponent<Transform>().localEulerAngles;

                angle = player.GetComponent<Transform>().localEulerAngles;
				//print(angle[0]);
				if(angle[0] < 60 && angle[0] > -60){
					newPos[0] = newPos[0] - 20;
				}
				else
					newPos[1] = newPos[1] + 20;

				angle= new Vector3(0,0,0);
				
                this.GetComponent<Transform>().localEulerAngles = angle;
				this.transform.position = newPos;
            }
    }

}
