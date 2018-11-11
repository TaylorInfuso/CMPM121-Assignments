/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBridge : MonoBehaviour
{
    DimTrigger _listener;
    public Collision col = null;
    public void Initialize(DimTrigger l)
    {
        _listener = l;
    }
    void OnCollisionEnter(Collision collision)
    {
        col = collision;
        _listener.OnCollisionEnter(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        col = null;
        _listener.OnCollisionExit(collision);
    }
 
    void OnTriggerEnter2D(Collider2D other)
    {
        _listener.OnTriggerEnter2D(other);
    }

}
*/