using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMoveOld
    :
    MonoBehaviour
{
    public Camera ViewCamera;
    public GameObject Child;
    public float MoveSpeed;
    public float JumpHeight;
    public float RotSpeed;

    bool jumping = false;
    Vector3 move = new Vector3( 0.0f,0.0f,0.0f );
    bool hideMouse = true;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            hideMouse = false;
        }
        if( Input.GetMouseButtonDown( 0 ) )
        {
            hideMouse = true;
        }
        Cursor.visible = !hideMouse;
    }

    // Using FixedUpdate makes everything much smoother.
    void FixedUpdate()
    {
        move = new Vector3( 0.0f,0.0f,0.0f );

        if( hideMouse )
        {
            float mouseMove = Mathf.Clamp( Input
                .GetAxis( "Mouse Y" ),-5.0f,5.0f );
            // print( mouseMove );

            float dt = Time.deltaTime;
            if( Time.deltaTime > 1.0f ) dt = 1.0f;

            ViewCamera.transform.eulerAngles -=
                    new Vector3( mouseMove,
                    0.0f,0.0f ) *
                    RotSpeed * dt;

            // 0 -> 70, 360 -> 290
            float rotX = ViewCamera.transform.eulerAngles.x;
            if( rotX > 60.0f && rotX < 90.0f )
            {
                rotX = 60.0f;
            }
            else if( rotX < 300.0f && rotX > 250.0f )
            {
                rotX = 300.0f;
            }

            ViewCamera.transform.eulerAngles = new
                Vector3( rotX,
                ViewCamera.transform.eulerAngles.y,
                ViewCamera.transform.eulerAngles.z );

            // print( ViewCamera.transform.localRotation.x );
            // float quatRotX = ViewCamera.transform.localRotation.x;
            // if( ( quatRotX > 0.0f && quatRotX < 0.5f ) ||
            //     ( quatRotX < 0.0f && quatRotX > -0.5f ) )
            // {
            //     // good
            // }

            // print( ViewCamera.transform.eulerAngles.x );

            transform.Rotate( Vector3.up,
                Input.GetAxis( "Mouse X" ) * 2.0f );
            Child.transform.Rotate( Vector3.up,
                -Input.GetAxis( "Mouse X" ) * 2.0f );
        }

        move.x = Input.GetAxis( "Horizontal" );
        move.z = Input.GetAxis( "Vertical" );

        if( Input.GetKey( KeyCode.Space ) )
        {
            jumping = true;
        }
        else jumping = false;

        if( jumping )
        {
            move.y += JumpHeight * Time.deltaTime;
        }

        transform.Translate( move * MoveSpeed * Time.deltaTime );
    }
}