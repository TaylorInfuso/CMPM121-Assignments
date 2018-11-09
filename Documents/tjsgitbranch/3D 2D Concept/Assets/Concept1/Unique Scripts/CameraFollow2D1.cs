using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D1 : MonoBehaviour
{

    public GameObject player;
    private bool flat = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && flat == true)
            this.GetComponent<Camera>().enabled = flat = false;
        else if (Input.GetKeyDown("f") && flat == false)
            this.GetComponent<Camera>().enabled = flat = true;

        var cameraLocation = this.gameObject.transform;
        var playerLocation = player.transform;
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 42);
    }
}
