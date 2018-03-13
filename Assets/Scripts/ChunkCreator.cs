using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCreator
    :
    MonoBehaviour
{
    public GameObject ChunkPrefab;

    List<GameObject> chunks = new List<GameObject>();
    int dir = 0;
    // Use this for initialization
    void Start()
    {
        CreateChunk( 0,0,0 );
        // CreateChunk( 25,0,0,20.0f );
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn chunks around player if they don't already exist.
        const int dist = 16;

        int xPos = ( int )Mathf.Floor( transform.position.x );
        int zPos = ( int )Mathf.Floor( transform.position.z );
        
        if( dir == 0 )
        {
            while( xPos % dist != 0 ) ++xPos;
            while( zPos % dist != 0 ) ++zPos;
        }
        else if( dir == 1 )
        {
            while( xPos % dist != 0 ) --xPos;
            while( zPos % dist != 0 ) --zPos;
        }
        else if( dir == 2 )
        {
            while( xPos % dist != 0 ) ++xPos;
            while( zPos % dist != 0 ) --zPos;
        }
        else if( dir == 3 )
        {
            while( xPos % dist != 0 ) --xPos;
            while( zPos % dist != 0 ) ++zPos;
        }
        ++dir;
        if( dir > 3 ) dir = 0;

        bool willGenerateChunk = true;
        foreach( GameObject c in chunks )
        {
            if( c.transform.position.x == xPos &&
                c.transform.position.z == zPos )
            {
                willGenerateChunk = false;
                break;
            }
        }

        if( willGenerateChunk )
        {
            CreateChunk( xPos,0,zPos );
        }
    }

    void CreateChunk( Vector3 pos )
    {
        chunks.Add( Instantiate( ChunkPrefab ) );
        chunks[chunks.Count - 1].GetComponent<Chunk>()
            .SetPos( pos );
    }

    void CreateChunk( int x,int y,int z )
    {
        CreateChunk( new Vector3( ( float )x,
            ( float )y,( float )z ) );
    }
}
