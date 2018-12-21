using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform checkpoint;
    private PlayerMovement2 pm;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.transform.position = checkpoint.position;
            print(checkpoint.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            pm.checkpos = transform.position;
            print(pm.checkpos);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
  
    
}
