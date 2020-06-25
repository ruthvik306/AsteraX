using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script which places this gameobject relative to another game object 
/// </summary>

public class PlayerShipExhaust : MonoBehaviour
{
    public Transform poi;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - poi.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = poi.position + offset;
    }
}
