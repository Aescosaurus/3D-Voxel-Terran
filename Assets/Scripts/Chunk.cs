using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Chunk
    :
    MonoBehaviour
{
    public GameObject CubePrefab;

    public int Width = 12;
    public int Length = 12;

    public bool SnapToGrid = true;

    public float Amplitude = 7.0f;
    public float Frequency = 12.0f;
    public bool RandomizeSeed = true;
    public float Seed = 99.0f;
    GameObject[] voxels;

    Vector3 voxelSize = new Vector3( 1.0f,1.0f,1.0f );
    Stopwatch watch = new Stopwatch();
    // 
    void Start()
    {
        // print( CubePrefab );
        voxels = new GameObject[Width * Length];

        if( RandomizeSeed )
        {
            Seed = Random.Range( 1.0f,1000.0f );
        }
        if( Width % 2 != 0 ) --Width;
        if( Length % 2 != 0 ) --Length;

        // Create voxels array.
        Generate();
        // Combine voxels' meshes into one for performance, then
        //  destroy them and leave just one mesh leftover.
        CombineVoxelsIntoMesh();
        DestroyVoxels();
    }

    void Update()
    {
    }

    void Generate()
    {
        // TODO: Optimize the HECK out of this.
        // var voxels = new GameObject[Width * Length];

        Vector3 voxelPos = new Vector3( transform.position.x,
            transform.position.y,transform.position.z );

        int i = 0;
        for( int x = 0; x < Width; ++x )
        {
            for( int z = 0; z < Length; ++z )
            {
                var curVoxel = voxels[i];
                curVoxel = Instantiate( CubePrefab );

                voxelPos = this.transform.position;
                voxelPos.y = 0.0f;
                voxelPos.x -= Width / 2.0f * voxelSize.x;
                voxelPos.z -= Length / 2.0f * voxelSize.z;

                voxelPos.x += x * voxelSize.x;
                voxelPos.z += z * voxelSize.z;

                // Snap to grid.
                voxelPos.x = Mathf.Round( voxelPos.x );
                voxelPos.z = Mathf.Round( voxelPos.z );

                // Perlin.
                voxelPos.y += Mathf.PerlinNoise( ( 0.0f +
                    this.transform.position.x + voxelPos.x ) / Frequency,
                    ( Seed + 0.0f + this.transform.position.z +
                    voxelPos.z ) / Frequency ) * Amplitude;

                voxelPos.y = Mathf.Floor( voxelPos.y );

                curVoxel.transform.position = voxelPos;
                curVoxel.transform.localScale = voxelSize;
                curVoxel.transform.parent = transform;
            }
        }
    }

    void CombineVoxelsIntoMesh()
    {
        StartWatch();
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        Mesh final = new Mesh();
        CombineInstance[] combiners = new CombineInstance[filters.Length];

        for( int i = 0; i < filters.Length; ++i )
        {
            combiners[i].subMeshIndex = 0;
            combiners[i].mesh = filters[i].sharedMesh;
            combiners[i].transform = filters[i].transform.localToWorldMatrix;
        }

        final.CombineMeshes( combiners );

        GetComponent<MeshFilter>().sharedMesh = final;
        gameObject.AddComponent<MeshCollider>();
    }

    void DestroyVoxels()
    {
        foreach( Transform child in transform )
        {
            // print( child );
            Destroy( child.gameObject );
        }
    }

    void StartWatch()
    {
        watch.Start();
    }

    void PrintWatch()
    {
        print( watch.ElapsedTicks / 10000.0f );
        watch.Stop();
        watch.Reset();
    }

    public Vector3 NearestValidPos( Vector3 input )
    {
        var x = input.x;
        // var y = input.y;
        var z = input.z;

        while( x % ( Width / 2 ) != 0 ) --x;
        while( z % ( Length / 2 ) != 0 ) --z;

        return( new Vector3( x,input.y,z ) );
    }

    public void SetPos( Vector3 pos )
    {
        transform.position = pos;
    }

    public void SetPos( Vector3 pos,float seed )
    {
        transform.position = pos;
        Seed = seed;
    }

    public void SetPosSafe( Vector3 pos )
    {
        transform.position = NearestValidPos( pos );
    }
}