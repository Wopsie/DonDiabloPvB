using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains function for mesh generation of the track.
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour{

    private MeshFilter filter;
    private MeshRenderer rend;
    [HideInInspector]
    public Vector3[] vertexOffsetVectors;
    
    private void Start(){
        //Load Leveldata for rendering the mesh
        //LevelData level = Resources.Load<LevelData>(dataName);

        if (!filter || !rend){
            filter = GetComponent<MeshFilter>();
            rend = GetComponent<MeshRenderer>();
        }

        //copy over values from level data for mesh generation
        Vector2[] pointsArr = LevelManager.Instance.level.points;
        float meshWidth = LevelManager.Instance.level.roadWidth;
        float texTiling = LevelManager.Instance.level.textureTiling;
        float pointSpacing = LevelManager.Instance.level.pointSpacing;

        //Set mesh filter mesh to generated mesh & texture it
        filter.mesh = CreateRoadMesh(pointsArr, false, meshWidth);
        int textureRepeat = Mathf.RoundToInt(texTiling * pointsArr.Length * pointSpacing * 0.05f);
        rend.sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
        //Debug.Log("RENDER MESH FROM START");
    }

    /// <summary>
    /// Creates road mesh with given parameters.
    /// </summary>
    /// <param name="points">Array of waypoints.</param>
    /// <param name="isClosed"></param>
    /// <param name="roadWidth">Width of road.</param>
    /// <returns></returns>
    public Mesh CreateRoadMesh(Vector2[] points, bool isClosed, float roadWidth){
        Vector3[] verts = new Vector3[points.Length * 2];
        Vector2[] uvs = new Vector2[verts.Length];
        vertexOffsetVectors = new Vector3[points.Length];
        int numTris = 2 * (points.Length - 1) + ((isClosed) ? 2 : 0);
        int[] tris = new int[numTris * 3];
        int vertIndex = 0;
        int trisIndex = 0;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 forward = Vector2.zero;
            if (i < points.Length - 1 || isClosed){
                forward += points[(i + 1) % points.Length] - points[i];
            }
            if (i > 0 || isClosed){
                forward += points[i] - points[(i - 1 + points.Length) % points.Length];
            }

            forward.Normalize();
            Vector2 left = new Vector2(-forward.y, forward.x);
            //store the left vector to calculate the edges of the mesh later
            vertexOffsetVectors[i] = left;
            verts[vertIndex] = points[i] + left * roadWidth * 0.5f;
            verts[vertIndex + 1] = points[i] - left * roadWidth * 0.5f;

            float completionPercent = i / (float)(points.Length - 1);
            float v = 1 - Mathf.Abs(2 * completionPercent - 1);
            uvs[vertIndex] = new Vector2(0, v);
            uvs[vertIndex + 1] = new Vector2(1, v);

            if (i < points.Length - 1 || isClosed){
                tris[trisIndex] = vertIndex;
                tris[trisIndex + 1] = (vertIndex + 2) % verts.Length;
                tris[trisIndex + 2] = vertIndex + 1;

                tris[trisIndex + 3] = vertIndex + 1;
                tris[trisIndex + 4] = (vertIndex + 2) % verts.Length;
                tris[trisIndex + 5] = (vertIndex + 3) % verts.Length;
            }

            vertIndex += 2;
            trisIndex += 6;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        return mesh;
    }
}
