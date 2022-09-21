using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PaintHole : MonoBehaviour
{
    public Terrain terrain;
    // Start is called before the first frame update

    public int circleRadius = 12;
    public float paintWeight = 0.001f;
    public float rayTimeInterval = 0.1f;

    private float rayTimer = 0f;
    private Vector3 rayHitPoint;
    private Vector3 heightmapPos;
    private float[,,] alphaData;
    private TerrainData tData;
    private float percentage;

    private const int DESERT = 0; //These numbers depend on the order in which
    private const int GRASS = 1; //the textures are loaded onto the terrain

    //  Persistant Functions
    //    ----------------------------------------------------------------------------


    void Start()
    {
        GetTerrainData();
        tData = terrain.terrainData;
        alphaData = tData.GetAlphamaps(0, 0, tData.alphamapWidth, tData.alphamapHeight);

    }


    void Update()
    {
        rayTimer += Time.deltaTime;

        if (rayTimer < rayTimeInterval)
            return;

        rayTimer = 0;

        RaycastToTerrain();
        GetHeightmapPosition();

        if (Input.GetMouseButton(0) && rayHitPoint != Vector3.zero)
            PaintCircle(heightmapPos);
    }


    //  Terrain Data
    //    ----------------------------------------------------------------------------


    private TerrainData terrainData;
    private Vector3 terrainSize;
    private int heightmapWidth;
    private int heightmapHeight;
    private float[,] heightmapData;
    private bool[,] b;


    void GetTerrainData()
    {
        if (!terrain)
            terrain = Terrain.activeTerrain;

        terrainData = terrain.terrainData;

        terrainSize = terrain.terrainData.size;

        heightmapWidth = terrain.terrainData.heightmapResolution;
        heightmapHeight = terrain.terrainData.heightmapResolution;

        heightmapData = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
        b = terrainData.GetHoles(0, 0, heightmapWidth - 1, heightmapHeight - 1);

    }


    //  Other Functions
    //    ----------------------------------------------------------------------------


    void RaycastToTerrain()
    {
        rayHitPoint = Vector3.zero;

        RaycastHit hit;
        Ray rayPos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(rayPos, out hit, Mathf.Infinity)) // also consider a layermask to just the terrain layer
        {
            return;
        }
        rayHitPoint = hit.point;
        Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, rayTimeInterval);
    }


    void GetHeightmapPosition()
    {
        // find the heightmap position of that hit
        heightmapPos.x = (rayHitPoint.x / terrainSize.x) * (float)(heightmapWidth);
        heightmapPos.z = (rayHitPoint.z / terrainSize.z) * (float)(heightmapHeight);

        // convert to integer
        heightmapPos.x = Mathf.RoundToInt(heightmapPos.x);
        heightmapPos.z = Mathf.RoundToInt(heightmapPos.z);

        // clamp to heightmap dimensions to avoid errors
        heightmapPos.x = Mathf.Clamp(heightmapPos.x, 0, heightmapWidth - 1);
        heightmapPos.z = Mathf.Clamp(heightmapPos.z, 0, heightmapHeight - 1);
    }


    void PaintCircle(Vector3 point)
    {
        int x;
        int z;
        int heightX;
        int heightZ;
        float heightY;
        Vector2 calc;
        // var b = new bool[heightmapWidth-1, heightmapHeight-1];
        percentage = (float)100 / 100f;
        for (z = -circleRadius; z <= circleRadius; z++)
        {
            for (x = -circleRadius; x <= circleRadius; x++)
            {
                // for a circle, calcualate a relative Vector2
                calc = new Vector2(x, z);
                // check if the magnitude is within the circle radius
                if (calc.magnitude <= circleRadius)
                {
                    // is within circle, paint height
                    heightX = (int)(point.x + x);
                    heightZ = (int)(point.z + z);

                    // check if heightX and Z is within the heightmapData array size
                    if (heightX >= 0 && heightX < heightmapWidth && heightZ >= 0 && heightZ < heightmapHeight)
                    {
                        // read current height
                        heightY = heightmapData[heightZ, heightX]; // note that in heightmapData, X and Z are reversed

                        // add paintWeight to the current height
                        heightY -= paintWeight;
                        // reach the bottom
                      //  Debug.Log(terrain.terrainData.GetHeight((int)heightmapPos.x, (int)heightmapPos.z));

                        // update heightmapData array
                        heightmapData[heightZ, heightX] = heightY;
                        if (terrain.terrainData.GetHeight((int)heightmapPos.x, (int)heightmapPos.z) < 3)
                            b[heightZ, heightX] = (heightX < 20 && heightX > 20 && heightZ < 20 && heightZ > 20);
                        // alphaData[heightZ, heightX, DESERT] = 1 - percentage;
                        //   alphaData[heightZ, heightX, GRASS] = percentage;
                    }



                }

            }
        }

        if (terrain.terrainData.GetHeight((int)heightmapPos.x, (int)heightmapPos.z) < 3) 
            terrain.terrainData.SetHoles(0, 0, b);

        // apply new heights to terrainData
        terrainData.SetHeights(0, 0, heightmapData);
        tData.SetAlphamaps(0, 0, alphaData);

    }
 
    private Vector3 GetTerrainSize()
    {
        return terrain.terrainData.size;
    }

    // Update is called once per frame

}
