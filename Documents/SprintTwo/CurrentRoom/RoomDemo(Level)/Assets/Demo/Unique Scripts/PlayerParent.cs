﻿/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{

    GameObject character;
    public GameObject ThicCharacter;
    private bool flat;
    Rigidbody2D rb;
    CapsuleCollider2D col;

    //Set speed of player
    public static float verSpeed = 6.0f;
    public static float horSpeed = 6.0f;

    // Use this for initialization
    void Start()
    {
        flat = false;
        character = this.gameObject;

        //rb = character.GetComponent<Rigidbody2D>();
        //col = character.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        character.GetComponent<Transform>().position = ThicCharacter.GetComponent<Transform>().position;
        character.GetComponent<Transform>().Translate(new Vector3(0, -1, 0));
        if (flat == false)
        {
            /*
            float straffe = Input.GetAxis("Vertical") * verSpeed; //y input
            float translation = Input.GetAxis("Horizontal") * horSpeed; //x input
            translation *= Time.deltaTime; //calculate distance
            straffe *= Time.deltaTime;
            translation = Mathf.Clamp(translation, translation, translation);
            character.transform.Translate(straffe, 0, -translation);
            //character.transform.position = ThicCharacter.transform.position;
            */
            /*
            if (character.transform.eulerAngles.x != 0)
            {
                this.gameObject.transform.Rotate(-90f, 0, 0);
                Vector3 scale = this.gameObject.transform.localScale;
                scale.z = 1f;
                this.gameObject.transform.localScale = scale;
            }
            if (Input.GetKeyDown("f"))
                flat = true;
        }
        else if (flat == true)
        {/*
            float translation = Input.GetAxis("Horizontal") * verSpeed; //y input
            translation *= Time.deltaTime; //calculate distance
            translation = Mathf.Clamp(translation, translation, translation);
            character.transform.Translate(translation, 0, 0);
            */
            /*
            if (character.transform.eulerAngles.x != 90)
            {
                this.gameObject.transform.Rotate(90f, 0, 0);
                Vector3 scale = this.gameObject.transform.localScale;
                scale.z = 1 / 181f;
                this.gameObject.transform.localScale = scale;
            }
            if (Input.GetKeyDown("f"))
                flat = false;
        }
    }
}
*/