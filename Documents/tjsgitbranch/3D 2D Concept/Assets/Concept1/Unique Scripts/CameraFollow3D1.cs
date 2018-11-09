using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow3D1 : MonoBehaviour {

    public GameObject player;
    private bool notFlat = true;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("f") && notFlat == true)
            this.GetComponent<Camera>().enabled = notFlat = false;
        else if (Input.GetKeyDown("f") && notFlat == false)
            this.GetComponent<Camera>().enabled = notFlat = true;

        var cameraLocation = this.gameObject.transform;
        var playerLocation = player.transform;
        gameObject.transform.position = new Vector3(player.transform.position.x - 15, player.transform.position.y + 5, player.transform.position.z);
	}
}
