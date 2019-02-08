using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotater : MonoBehaviour
{
    public GameObject player;
    public GameObject puzzleBlock;
    //public Text doorText;

    Vector3 angle = new Vector3();
    public float rotationUpAndDown = 0;
    Vector3 addAngle;
    public float RotationSpeed;
    private Vector3 offset;

    private float mouseY;

    private void Start()
    {
        player = GameObject.Find("Player");

        offset = player.transform.position - transform.Find("Main Camera").transform.position;
    }
    // Update is called once per frame
    void Update()
    {

        if (GlobalVariables.doorNum <= 0)
        {

            if (player.GetComponent<PlayerMovement2>().flat == false)
            {
                float desiredYAngle = player.transform.eulerAngles.y;
                float desiredXAngle = transform.eulerAngles.z;

                Quaternion rotation = Quaternion.Euler(-desiredXAngle, desiredYAngle + 90, 0);
                Vector3 desiredPosition = player.transform.position - (rotation * offset);

                CompensateForWall(transform.position, ref desiredPosition);

                desiredPosition = desiredPosition + (rotation * offset * 0.1f); // prevent camera going into to the wall

                if (Input.GetMouseButton(0))
                {
                    if (Input.mousePosition.y > mouseY && rotationUpAndDown > -180 / 20)
                    {
                        rotationUpAndDown -= 1.1f;
                    }
                    else if (Input.mousePosition.y < mouseY && rotationUpAndDown < 180 / 4)
                    {
                        rotationUpAndDown += 1.1f;
                    }
                }

                mouseY = Input.mousePosition.y;

                if (Input.GetKey("up") && rotationUpAndDown < 180 / 4)
                {
                    rotationUpAndDown += 1f;
                }
                if (Input.GetKey("down") && rotationUpAndDown > -180 / 20)
                {
                    rotationUpAndDown -= 1f;
                }

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, player.GetComponent<PlayerMovement2>().rotationLeftAndRight, -rotationUpAndDown), Time.deltaTime * 10.0f);

                transform.Find("Main Camera").transform.position = desiredPosition;

                transform.Find("Main Camera").transform.LookAt(transform);
            }
		else{
			this.transform.localEulerAngles = new Vector3 (0,0,0);
        }

		}
	}

    void CompensateForWall (Vector3 fromObject, ref Vector3 toTarget)
    {
        print("yolo");
        Debug.DrawLine(fromObject, toTarget, Color.red);

        RaycastHit wallHit = new RaycastHit();

        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.white);
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
            Debug.DrawRay(toTarget, Vector3.left, Color.green);
        }
    }
}