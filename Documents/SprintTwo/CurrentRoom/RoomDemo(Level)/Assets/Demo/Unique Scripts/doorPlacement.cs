using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorPlacement : MonoBehaviour
{

    public GameObject zone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        
        Vector3 ang = this.gameObject.transform.localEulerAngles;
        new WaitForSeconds(0.05f);
        zone.gameObject.GetComponent<doorZone>().doorAngle = ang;
        zone.gameObject.GetComponent<doorZone>().OnMouseDown();
		this.gameObject.transform.position = new Vector3(0, -500, 0);
    }
}
