using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove
    :
    MonoBehaviour
{
    public float JumpHeight;

    bool jumping = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if( Random.Range( 0,100 ) < 10 )
        {
            jumping = !jumping;
        }

        if( jumping )
        {
            transform.Translate( new Vector3( 0.0f,JumpHeight,0.0f ) *
                Time.deltaTime );
        }
    }
}
