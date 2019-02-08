using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMovement : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameObject.GetComponent<PlayerMovement2>().flat == true)
        {
            Vector3 temp = player.transform.position;
            temp[1] = temp[1] + 20;
            this.transform.position = temp;
        }
            
    }
}
