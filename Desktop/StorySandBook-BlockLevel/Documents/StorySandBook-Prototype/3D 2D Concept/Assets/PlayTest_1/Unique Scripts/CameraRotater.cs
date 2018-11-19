using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour {

    public GameObject player;
    public float rotationUpAndDown = 0;
    private float mouseY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.y > mouseY && rotationUpAndDown > -Mathf.PI / 20)
            {
                rotationUpAndDown -= 0.065f;
            }
            else if (Input.mousePosition.y < mouseY && rotationUpAndDown < Mathf.PI / 4)
            {
                rotationUpAndDown += 0.065f;
            }
        }

        mouseY = Input.mousePosition.y;

        if (Input.GetKey("up") && rotationUpAndDown < Mathf.PI/4)
        {
            rotationUpAndDown += 0.05f;
        }
        if (Input.GetKey("down") && rotationUpAndDown > - Mathf.PI/20)
        {
            rotationUpAndDown -= 0.05f;
        }
        this.transform.position = player.transform.position;
        var angleLeftAndRight = (player.GetComponent<PlayerMovement2>().rotationLeftAndRight) / Mathf.PI * 180 + 90;
        var angleUpAndDown = rotationUpAndDown / Mathf.PI * 180;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(angleUpAndDown, angleLeftAndRight, 0), Time.deltaTime * 10.0f);
        
    }
}
