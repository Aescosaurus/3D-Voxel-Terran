using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLogger
    :
    MonoBehaviour
{
    float total = 0.0f;
    int nInputs = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        total += ( 1.0f / Time.deltaTime );
        ++nInputs;
        print( total / ( float )nInputs );
    }
}
