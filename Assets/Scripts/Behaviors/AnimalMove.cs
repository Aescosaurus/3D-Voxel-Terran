using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove
    :
    MonoBehaviour
{
    public float JumpHeight;
    public float MoveSpeed;
    public float RotAmount;

    bool jumping = false;
    bool canJump = false;
    bool moving = false;
    bool rotating = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector3( 0.0f,0.0f,0.0f );
        if( Random.Range( 0,1000 ) < 5 && canJump )
        {
            jumping = true;
            canJump = false;
        }
        if( Random.Range( 0,1000 ) < 50 )
        {
            moving = !moving;
        }
        if( Random.Range( 0,1000 ) < 100 )
        {
            rotating = !rotating;
            if( Random.Range( 0,10 ) > 5 ) RotAmount *= -1;
        }

        if( jumping ) move.y = JumpHeight;
        if( moving ) move.z += MoveSpeed;
        if( rotating ) transform.Rotate( Vector3.up,RotAmount );
        // print( RotAmount );
        
        transform.Translate( move * Time.deltaTime );
    }

    void OnCollisionEnter( Collision other )
    {
        jumping = false;
        canJump = true;
    }
}
