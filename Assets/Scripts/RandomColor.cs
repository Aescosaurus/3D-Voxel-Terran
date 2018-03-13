using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor
    :
    MonoBehaviour
{
    public Material[] Mats;
    // Use this for initialization
    void Start()
    {
        GetComponent<MeshRenderer>()
            .material = Mats[( int )Random
            .Range( 0,Mats.Length )];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
