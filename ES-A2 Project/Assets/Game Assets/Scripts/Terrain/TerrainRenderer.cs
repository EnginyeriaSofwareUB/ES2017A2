using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[ExecuteInEditMode()]
public class TerrainRenderer : MonoBehaviour {
    public PolygonCollider2D polygonCollider2D;
    public Color color = Color.green;
    public GameObject meshRendererPrefab;

    void Awkae() {
        this.polygonCollider2D = this.GetComponent<PolygonCollider2D>();
    }

    void Update() {
        if(this.polygonCollider2D == null) {
            this.polygonCollider2D = this.GetComponent<PolygonCollider2D>();
        }
        this.renderPolygon();
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < this.polygonCollider2D.pathCount; i++) {
            Vector2[] path = this.polygonCollider2D.GetPath(i);
            for (int j = 0; j < path.Length; j++) {
                Vector2 firstPoint = this.transform.TransformPoint(path[j]);
                Vector2 nextPoint = this.transform.TransformPoint(path[(j + 1) % path.Length]);

                Gizmos.color = this.color;
                this.DrawLine(firstPoint, nextPoint, 5);
            }
        }
    }
    public void DrawLine(Vector3 p1, Vector3 p2, int width) {
        for (int i = 0; i < width; i++) {
            Gizmos.DrawLine(p1 - (p1 * i * 0.001f), p2 - (p2 * i * 0.001f));
        }
    }


    private void renderPolygon() {
        List<MeshFilter> meshFilterList = this.equalizeMeshFiltersToPathCount();

        for (int i = 0; i < this.polygonCollider2D.pathCount; i++) {
            MeshFilter meshFilter = meshFilterList[i];
            Mesh mesh = new Mesh();
            Vector2[] path = this.polygonCollider2D.GetPath(i);
            int pointCount = path.Length;

            Vector3[] vertices = new Vector3[pointCount];
            Vector2[] uv = new Vector2[pointCount];
            for (int j = 0; j < pointCount; j++) {
                Vector2 actual = path[j];
                vertices[j] = new Vector3(actual.x, actual.y, 0);
                uv[j] = actual;
            }
            Triangulator tr = new Triangulator(path);
            int[] triangles = tr.Triangulate();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            meshFilter.mesh = mesh;
        }
    }

    private List<MeshFilter> equalizeMeshFiltersToPathCount() {
        int pathCount = this.polygonCollider2D.pathCount;
        List<MeshFilter> meshFilterList = new List<MeshFilter>();
        meshFilterList.AddRange(this.GetComponentsInChildren<MeshFilter>());

        while (pathCount != meshFilterList.Count) {
            if (pathCount < meshFilterList.Count) {
                DestroyImmediate(meshFilterList[0].gameObject);
                meshFilterList.RemoveAt(0);
            } else if (pathCount > meshFilterList.Count) {
                GameObject meshContainer = Instantiate<GameObject>(this.meshRendererPrefab);
                meshContainer.transform.parent = this.transform;
                meshContainer.transform.position = this.transform.position;
                meshFilterList.Add(meshContainer.GetComponent<MeshFilter>());
            }
        }

        return meshFilterList;
    }
}
