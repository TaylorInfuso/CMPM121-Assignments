using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{

    //public GameObject player;
    Vector3 angle = new Vector3();
    Vector3 Rotation3D;
    Vector3 Rotation2D;
    Camera playCam;
    private float aniSpeed = 30;

    private void Start()
    {
        Rotation3D = new Vector3(0, 90, 0);
        Rotation2D = new Vector3(-10, 0, 0);
        playCam = gameObject.GetComponentInChildren<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<PlayerMovement2>().flat == false)
        {

            //var playCamLocation = this.gameObject.transform;
            angle = this.GetComponent<Transform>().localEulerAngles;
            //var playerLocation = player.transform;
            //playCam.transform.localPosition = new Vector3(0, 5, -15);
            //gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            if (angle.x < Rotation3D.x)
            {
                angle.x -= (Rotation2D.x - Rotation3D.x) / aniSpeed;
                angle.y += (Rotation3D.y - Rotation2D.y) / aniSpeed;
            } else
            {
                angle = Rotation3D;
            }
            this.GetComponent<Transform>().localEulerAngles = angle;
        } else
        {
            angle = this.GetComponent<Transform>().localEulerAngles;
            //var playerLocation = player.transform;
            //gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            //playCam.transform.localPosition = new Vector3(0, 0, 20);
            if (angle != Rotation2D)
                angle = Rotation2D;
            this.GetComponent<Transform>().localEulerAngles = angle;

        }
    }
}
