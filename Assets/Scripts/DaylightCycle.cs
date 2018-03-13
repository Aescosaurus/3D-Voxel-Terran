using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycle
    :
    MonoBehaviour
{
    public GameObject GlobalLight;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GlobalLight.transform.Rotate( Vector3.left,
            Time.deltaTime );
    }
}
