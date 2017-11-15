using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(PolygonCollider2D))]
[ExecuteInEditMode()]
public class Terrain2D : MonoBehaviour {
    public PolygonCollider2D polygonCollider2D;
    public Color color = Color.green;
    public float angleDiference;
    public List<String> destructionTags;
    public GameObject meshRendererPrefab;

    void Awkae() {
        if (this.polygonCollider2D == null) {
            polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
        }
    }

    void Update() {
        if (this.polygonCollider2D == null) {
            polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
        }
        this.renderPolygon();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (this.destructionTags.Contains(other.gameObject.tag)) {
            CircleCollider2D circle2D = other.gameObject.GetComponent<CircleCollider2D>();
            Vector2 center = other.transform.TransformPoint(circle2D.offset);
            float radius = circle2D.radius * Mathf.Max(circle2D.transform.localScale.x, circle2D.transform.localScale.y);
            PolygonCollider2D onePathCollider = this.gameObject.AddComponent<PolygonCollider2D>();
            List<List<Vector2>> newPaths = new List<List<Vector2>>();

            for (int i = 0; i < this.polygonCollider2D.pathCount; i++) {
                List<List<int>> points = new List<List<int>>();
                Vector2[] oldPath = this.polygonCollider2D.GetPath(i);
                List<Vector2> newPath = new List<Vector2>();
                onePathCollider.SetPath(0, oldPath);

                for (int j = 0; j < oldPath.Length; j++) {
                    Vector2 firstPoint = oldPath[j];
                    Vector2 nextPoint = oldPath[(j + 1) % oldPath.Length];

                    this.addPointIfOutsideExplosion(circle2D, firstPoint, newPath);
                    int firstImpactPointIndex = this.addExplosionSurfacePointsToPath(onePathCollider, circle2D, center, radius, firstPoint, nextPoint, newPath);
                    Vector2 lastImpactPoint = this.addExplosionLastPointToPath(circle2D, firstPoint, nextPoint, newPath);

                    if (firstImpactPointIndex != -1) {
                        List<int> pointsEnds = new List<int>() {
                            firstImpactPointIndex,
                            newPath.Count - 1
                        };
                        points.Add(pointsEnds);
                    }
                }
                /*
                if (points.Count == 0) {
                    newPaths.Add(newPath);
                } else {
                    newPaths.AddRange(this.separateTerrain(newPath, circle2D, points, oldPath).ToArray());
                }*/
                newPaths.Add(newPath);
            }
            Destroy(onePathCollider);

            this.polygonCollider2D.pathCount = newPaths.Count;
            for(int i = 0; i < newPaths.Count; i++) {
                this.polygonCollider2D.SetPath(i, newPaths[i].ToArray());
            }
            Destroy(other.gameObject);
        }
    }

    private List<List<Vector2>> separateTerrain(List<Vector2> path, CircleCollider2D circle2D, List<List<int>> points, Vector2[] oldPath) {
        Vector2[] pathArray = path.ToArray();
        List<List<Vector2>> newPaths = new List<List<Vector2>>();

        for (int i = 0; i < points.Count; i++) {
            List<int> ends = points[i];
            List<Vector2> newPath = new List<Vector2>();
            int firstIndex;
            int nextIndex;
            if (i == 0) {
                firstIndex = (points[points.Count - 1][1] + 1) % (pathArray.Length);
                nextIndex = ends[1];
            } else {
                firstIndex = (points[i - 1][1] + 1) % (pathArray.Length);
                nextIndex = ends[1]; 
            }

            List<Vector2> splitPath = new List<Vector2>();
            while (firstIndex != nextIndex) {
                splitPath.Add(pathArray[firstIndex]);
                firstIndex = (firstIndex + 1) % (pathArray.Length);
            }

            newPaths.Add(splitPath);
            /*
            if (splitPath.Count != 0) {
                //splitPath.Add(pathArray[nextIndex]);

                GameObject gameObject = new GameObject();
                PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
                collider.SetPath(0, splitPath.ToArray());
            }*/
        }
        return newPaths;
    }

        private Vector2 addExplosionLastPointToPath(CircleCollider2D circle2D, Vector2 firstPoint, Vector2 nextPoint, List<Vector2> path) {
        Vector2 collPoint = Vector2.zero;
        if (!circle2D.OverlapPoint(this.transform.TransformPoint(nextPoint))) {
            collPoint = this.getCollisionPoint(nextPoint, firstPoint);
            if (collPoint != Vector2.zero) {
                this.addWorldPointToPath(path, collPoint);
            }
        }
        return collPoint;
    }

    private int addExplosionSurfacePointsToPath(PolygonCollider2D terrain, CircleCollider2D circle2D, Vector2 center, float radius, Vector2 firstPoint, Vector2 nextPoint, List<Vector2> path) {
        EdgeCollider2D twoPointEdge = this.create2PointEdgeCollider(firstPoint, nextPoint);
        Vector2 firstImpactPoint = Vector2.zero;
        int firstImpactPointIndex = -1;
        if (twoPointEdge.bounds.Intersects(circle2D.bounds) && !circle2D.OverlapPoint(this.transform.TransformPoint(firstPoint))) {
            firstImpactPoint = this.getCollisionPoint(firstPoint, nextPoint);
            float angle = getAngle(firstImpactPoint, center, radius);

            if (firstImpactPoint != Vector2.zero) {
                this.addWorldPointToPath(path, firstImpactPoint);
                firstImpactPointIndex = path.Count - 1;
                this.addNextExplosionPointsToPath(terrain, path, ref angle, center, radius);
            }
        }
        Destroy(twoPointEdge);
        return firstImpactPointIndex;
    }

    private EdgeCollider2D create2PointEdgeCollider(Vector2 firstPoint, Vector2 nextPoint) {
        List<Vector2> twoPointEdgePoints = new List<Vector2> {
            firstPoint,
            nextPoint
        };
        EdgeCollider2D twoPointEdge = gameObject.AddComponent<EdgeCollider2D>();
        twoPointEdge.points = twoPointEdgePoints.ToArray();
        return twoPointEdge;
    }

    private void addPointIfOutsideExplosion(CircleCollider2D circle2D, Vector2 point, List<Vector2> path) {
        if (!circle2D.OverlapPoint(this.transform.TransformPoint(point))) {
            path.Add(point);
        }
    }

    private void addWorldPointToPath(List<Vector2> path, Vector2 point) {
        path.Add(this.transform.InverseTransformPoint(point));
    }

    private void addNextExplosionPointsToPath(PolygonCollider2D terrain, List<Vector2> path, ref float angle, Vector2 center, float radius) {
        Vector2 surfacePointCircle = this.getNextPointOnCircle(ref angle, center, radius);
        while (terrain.OverlapPoint(surfacePointCircle)) {
            this.addWorldPointToPath(path, surfacePointCircle);
            surfacePointCircle = this.getNextPointOnCircle(ref angle, center, radius);
        }
    }

    private Vector2 getNextPointOnCircle(ref float angle, Vector2 center, float radius) {
        angle += 0.1f;
        float x = center.x + (radius) * Mathf.Cos(angle);
        float y = center.y + (radius) * Mathf.Sin(angle);
        return new Vector2(x, y);
    }

    private Vector2 getCollisionPoint(Vector2 point1, Vector2 point2) {
        RaycastHit2D[] hitInfo = Physics2D.LinecastAll(this.transform.TransformPoint(point1), this.transform.TransformPoint(point2));
        foreach(RaycastHit2D info in hitInfo) {
            if(this.destructionTags.Contains(info.collider.tag)) {
                return info.point;
            }
        }
        return Vector2.zero;
    }

    private float getAngle(Vector2 surf, Vector2 center, float radius) {
        float angle = (Mathf.Atan2(surf.y - center.y, surf.x - center.x)) % (2 * Mathf.PI);
        if(angle < 0) {
            angle += 2 * Mathf.PI;
        }
        return angle;
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
}