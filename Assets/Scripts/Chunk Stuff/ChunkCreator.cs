using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCreator
    :
    MonoBehaviour
{
    public GameObject ChunkPrefab;
    Chunk chunkScript;

    List<GameObject> chunks = new List<GameObject>();
    // int dir = 0;
    // Use this for initialization
    void Start()
    {
        chunkScript = ChunkPrefab.GetComponent<Chunk>();
        CreateChunk( 0,0,0 );
        // CreateChunk( 25,0,0,20.0f );
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn chunks around player if they don't already exist.
        // const int dist = 16;

        int xPos = ( int )Mathf.Floor( transform.position.x );
        int zPos = ( int )Mathf.Floor( transform.position.z );

        var spawnPos = chunkScript.NearestValidPos( new
            Vector3( xPos,0.0f,zPos ) );

        bool willGenerateChunk = true;
        foreach( GameObject c in chunks )
        {
            // var temp = chunkScript
            //     .NearestValidPos( spawnPos );
            var temp = spawnPos;
            if( c.transform.position.x == temp.x &&
                c.transform.position.z == temp.z )
            {
                willGenerateChunk = false;
                break;
            }
        }

        // print( spawnPos );

        if( willGenerateChunk )
        {
            CreateChunk( spawnPos );
        }
    }

    void CreateChunk( Vector3 pos )
    {
        chunks.Add( Instantiate( ChunkPrefab ) );
        chunks[chunks.Count - 1].GetComponent<Chunk>()
            .SetPosSafe( pos );
    }

    void CreateChunk( int x,int y,int z )
    {
        CreateChunk( new Vector3( ( float )x,
            ( float )y,( float )z ) );
    }
}
