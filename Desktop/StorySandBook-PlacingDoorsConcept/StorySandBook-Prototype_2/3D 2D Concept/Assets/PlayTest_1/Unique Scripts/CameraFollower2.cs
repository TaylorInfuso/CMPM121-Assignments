using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower2: MonoBehaviour
{
    public GameObject player;
    //public GameObject player;
    Vector3 angle = new Vector3();
    public float rotationUpAndDown = 0;
    Vector3 Rotation3D;
    Vector3 Rotation2D;
    Vector3 Position3D;
    Vector3 Position2D;
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
        playCam = gameObject.GetComponentInChildren<Camera>();
    }
    // Update is called once per frame
    void Update()
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

            //var playCamLocation = this.gameObject.transform;
            angle = this.GetComponent<Transform>().localEulerAngles;
            //var playerLocation = player.transform;
            //playCam.transform.localPosition = new Vector3(0, 5, -15);
            //gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            //if (angle.x < Rotation3D.x)
            // {
            //   angle.x -= (Rotation2D.x - Rotation3D.x) / aniSpeed;
            //  angle.y += (Rotation3D.y - Rotation2D.y) / aniSpeed;
            ///} else
            //{
            //angle = Rotation3D;
            ///}

            this.GetComponentInChildren<Camera>().GetComponent<Transform>().localPosition = Position3D;
            this.GetComponent<Transform>().localEulerAngles = angle;
            //Debug.Log(Vector2.Angle(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(playCam.transform.position.x, playCam.transform.position.z)));
            //playCam.transform.rotation = Quaternion.Slerp(playCam.transform.rotation, Quaternion.Euler(0, Vector.Angle(player.transform.position, playCam.transform.position), 0), Time.deltaTime * 10.0f);
            //playCam.transform.position = new Vector3(playCam.transform.position.x, playCam.transform.position.y, playCam.transform.position.z);
        }
        else
        {
            angle = this.GetComponent<Transform>().localEulerAngles;
            //var playerLocation = player.transform;
            //gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            //playCam.transform.localPosition = new Vector3(0, 0, 20);
            if (angle != Rotation2D)
                angle = Rotation2D;

            this.GetComponentInChildren<Camera>().GetComponent<Transform>().localPosition = Position2D;
            this.GetComponent<Transform>().localEulerAngles = angle;
            //playCam.transform.rotation = Quaternion.Slerp(playCam.transform.rotation, Quaternion.Euler(90, (player.GetComponent<PlayerMovement2>().rotationAngle) / Mathf.PI * -180, 0), Time.deltaTime * 10.0f);

        }
    }
}