using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript
    :
    MonoBehaviour
{
    public Material Grass;
    public Material Dirt;
    public Material Stone;
    public Material UsedMat;

    public MeshRenderer mr;
    // Use this for initialization
    void Start()
    {
        mr.material = UsedMat;
        // if( transform.position.y < 2 )
        // {
        //     mr.material = Stone;
        // }
        // else
        // {
        //     mr.material = Grass;
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
