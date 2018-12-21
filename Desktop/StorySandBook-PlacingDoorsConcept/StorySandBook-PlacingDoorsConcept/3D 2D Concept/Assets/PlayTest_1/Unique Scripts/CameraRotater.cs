using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotater : MonoBehaviour
{
    public GameObject player;
    public Text doorText;

    Vector3 angle = new Vector3();
    public float rotationUpAndDown = 0;
    Vector3 Rotation3D;
    Vector3 Rotation2D;
    Vector3 Position3D;
    Vector3 Position2D;
    Vector3 doorHeight;
    Camera playCam;
    bool temp = false;
    private float aniSpeed = 30;
    private float mouseY;

    private void Start()
    {

        Rotation3D = new Vector3(0, 90, 0);
        Rotation2D = new Vector3(-10, 0, 0);
        Position3D = new Vector3(0, 6, -15);
        Position2D = new Vector3(0, 6, -20);
        doorHeight = new Vector3(0, 50, 0);
        playCam = gameObject.GetComponentInChildren<Camera>();
    }
    // Update is called once per frame
    void Update()
    {

        if (GlobalVariables.doorNum <= 0)
        {
            this.transform.position = player.transform.position;
            if (gameObject.GetComponentInParent<PlayerMovement2>().flat == false)
            {

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

                if (Input.GetKey("up") && rotationUpAndDown < Mathf.PI / 4)
                {
                    rotationUpAndDown += 0.05f;
                }
                if (Input.GetKey("down") && rotationUpAndDown > -Mathf.PI / 20)
                {
                    rotationUpAndDown -= 0.05f;
                }
                var angleLeftAndRight = (player.GetComponent<PlayerMovement2>().rotationLeftAndRight) / Mathf.PI * 180 + 90;
                var angleUpAndDown = rotationUpAndDown / Mathf.PI * 180;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(angleUpAndDown, angleLeftAndRight, 0), Time.deltaTime * 10.0f);

                angle = this.GetComponent<Transform>().localEulerAngles;

                this.GetComponentInChildren<Camera>().GetComponent<Transform>().localPosition = Position3D;
                this.GetComponent<Transform>().localEulerAngles = angle;
            }
            else
            {
                angle = this.GetComponent<Transform>().localEulerAngles;
                if (angle != Rotation2D)
                    angle = Rotation2D;

                this.GetComponentInChildren<Camera>().GetComponent<Transform>().localPosition = Position2D;
                this.GetComponent<Transform>().localEulerAngles = angle;
            }
        }
        else
        {
            doorText.text = "Number of Doors: " + GlobalVariables.doorNum.ToString();
            angle = this.GetComponent<Transform>().localEulerAngles;
            angle = new Vector3(90, 0, 0);
            this.GetComponent<Transform>().localEulerAngles = angle;
            Vector3 loc = transform.position;
            loc[1] = 30;
            transform.position = loc;
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
    }
}