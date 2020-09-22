using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]

public class MeshTest : MonoBehaviour
{
    public int tilesX = 64;
    public int tilesZ = 64;

    public float tileWidth = 2.0f;
    public float tileHeight = 2.0f;

    public float noiseScale = 8.0f;

    public float noiseRX = 2.0f;
    public float noiseRZ = 2.0f;

    public float noiseWeight = 12.0f;

    public float posterizeLevels = 8.0f;

    Vector2 i2pt(int idx)
    {
        int vertsX = tilesX + 1;
        int vertsZ = tilesZ + 1;
        return new Vector2(
            (idx % vertsX) * tileWidth,
            (idx / vertsX) * tileHeight
        );
    }

    int pt2i(Vector2 pt)
    {
        int vertsX = tilesX + 1;
        int vertsZ = tilesZ + 1;
        int idx = (int)(Mathf.Floor(pt.y / tileHeight) * vertsX) + (int)Mathf.Floor(pt.x / tileWidth);
        return idx;
    }

    void drawDVert(float x, float z, Color col)
    {
        Debug.DrawLine(new Vector3(x, 0, z), new Vector3(x + 0.1f, 0, z + 0.1f), col, 5);
    }

    void drawDLine(float x1, float z1, float x2, float z2, Color col)
    {
        Debug.DrawLine(new Vector3(x1, 0, z1), new Vector3(x2, 0, z2), col, 5);
    }

    Vector2 rotatePtCW(Vector2 pt)
    {
        return new Vector2(pt.y, -pt.x);
    }

    Vector2 translatePt(Vector2 pt)
    {
        //Debug.Log("x " + pt.x + " y " + pt.y);
        return new Vector2(pt.x + tileWidth, pt.y + tileHeight);
    }

    Vector2[] rotateMeshQuadrant(Vector2[] vArray)
    {
        return new Vector2[]{
            rotatePtCW(vArray[0]), rotatePtCW(vArray[1]), rotatePtCW(vArray[2]),
            rotatePtCW(vArray[3]), rotatePtCW(vArray[4]), rotatePtCW(vArray[5])
        };
    }

    Vector2[] translateMeshQuadrant(Vector2[] vArray)
    {
        return new Vector2[]{
            translatePt(vArray[0]), translatePt(vArray[1]), translatePt(vArray[2]),
            translatePt(vArray[3]), translatePt(vArray[4]), translatePt(vArray[5])
        };
    }



    void updateMesh()
    {
        MeshRenderer meshRenderer = gameObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        MeshFilter meshFilter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
        Mesh mesh = new Mesh();

        int vertsX = tilesX + 1;
        int vertsZ = tilesZ + 1;

        Vector3[] vertices = new Vector3[vertsX * vertsZ];

        for (int j = 0; j < vertsZ; j++)
        {
            for (int i = 0; i < vertsX; i++)
            {
                int idx = j * vertsX + i;
                float vertX = i * tileWidth;
                float vertZ = j * tileHeight;

                float vertY = Mathf.Floor(Mathf.PerlinNoise(
                    Mathf.Floor(vertX / noiseRX) * noiseRX * noiseScale / 100,
                    Mathf.Floor(vertZ / noiseRZ) * noiseRZ * noiseScale / 100) * posterizeLevels) / posterizeLevels;
                //drawDVert(vertX, vertZ, Color.red);
                vertices[idx] = new Vector3(vertX, vertY * noiseWeight, vertZ);
            }
        }

        Debug.Log("verts " + vertices.Length);

        mesh.vertices = vertices;

        Vector2[] meshQuadrant = new Vector2[]{
            new Vector2(0,0), new Vector2(0,tileHeight), new Vector2(tileWidth,tileHeight),
            new Vector2(0,0), new Vector2(tileWidth,tileHeight), new Vector2(tileWidth,0)
        };

        int[] tris = new int[tilesX * tilesZ * 3 * 2];

        for (int j = 0; j < tilesZ; j++)
        {
            for (int i = 0; i < tilesX; i++)
            {
                int triIndex = j * tilesX + i;
                int tIndex = triIndex * 3 * 2;

                float x1 = i * tileWidth;
                float x2 = (i + 1) * tileWidth;
                float z1 = j * tileWidth;
                float z2 = (j + 1) * tileWidth;

                int tOffset = ((i + j) % 2);

                if (tOffset == 0)
                {
                    tris[tIndex] = pt2i(new Vector2(x1, z1));
                    tris[tIndex + 1] = pt2i(new Vector2(x1, z2));
                    tris[tIndex + 2] = pt2i(new Vector2(x2, z2));
                    tris[tIndex + 3] = pt2i(new Vector2(x1, z1));
                    tris[tIndex + 4] = pt2i(new Vector2(x2, z2));
                    tris[tIndex + 5] = pt2i(new Vector2(x2, z1));
                }
                else
                {
                    tris[tIndex] = pt2i(new Vector2(x1, z1));
                    tris[tIndex + 1] = pt2i(new Vector2(x1, z2));
                    tris[tIndex + 2] = pt2i(new Vector2(x2, z1));
                    tris[tIndex + 3] = pt2i(new Vector2(x1, z2));
                    tris[tIndex + 4] = pt2i(new Vector2(x2, z2));
                    tris[tIndex + 5] = pt2i(new Vector2(x2, z1));
                }
            }
        }

        Debug.Log("tris " + tris.Length);

        mesh.triangles = tris;

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        Color32[] colors = new Color32[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            float v1 = vertices[i].y / noiseWeight;
            //colors[i] = new Color(v1, v1, v1, 255);
            colors[i] = Color.HSVToRGB(Mathf.Clamp(v1 * 1.5f - 0.5f, 0.0f, 1.0f), 1.0f - v1, Mathf.Clamp(v1 * 2.0f, 0.0f, 1.0f));
        }

        mesh.colors32 = colors;

        if (Application.IsPlaying(gameObject))
        {
            Destroy(gameObject.GetComponent<MeshCollider>());
            var collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = mesh;
        }
        else DestroyImmediate(gameObject.GetComponent<MeshCollider>());

    }

    void Start()
    {
        updateMesh();
    }
    void OnValidate()
    {
        updateMesh();
    }
}