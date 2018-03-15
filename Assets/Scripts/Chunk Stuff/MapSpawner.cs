﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Spawns chunks within a specified range.
/// </summary>
public class MapSpawner
    :
    MonoBehaviour
{
    public GameObject World;
    public GameObject ChunkPrefab;
    public Vector3 min;
    public Vector3 max;

    List<GameObject> chunks = new List<GameObject>();
    Chunk chunkScript;
    // Use this for initialization
    void Start()
    {
        chunkScript = ChunkPrefab.GetComponent<Chunk>();

        for( float x = min.x; x < max.x; x += chunkScript.Width / 2 )
        {
            for( float z = min.z; z < max.z; z += chunkScript.Length / 2 )
            {
                chunks.Add( Instantiate( ChunkPrefab ) );

                chunks[chunks.Count - 1].GetComponent<Chunk>()
                    .SetPosSafe( new Vector3( x,0.0f,z ) );

                chunks[chunks.Count - 1].transform
                    .SetParent( World.transform );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
