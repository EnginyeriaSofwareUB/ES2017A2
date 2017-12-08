using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Terrain2D : MonoBehaviour {
    public PolygonCollider2D polygonCollider2D;
    public float angleDiference;
    public List<String> destructionTags;
    public bool working = false;

    void Awkae() {
        this.polygonCollider2D = this.GetComponent<PolygonCollider2D>();
    }

    void Update() {
        if (this.polygonCollider2D == null) {
            this.polygonCollider2D = this.GetComponent<PolygonCollider2D>();
        }
    }

    struct SeparationPoints {
        int firstIndex;
        int lastIndex;

        public int FirstIndex {
            get {
                return this.firstIndex;
            }

            set {
                this.firstIndex = value;
            }
        }

        public int LastIndex {
            get {
                return this.lastIndex;
            }

            set {
                this.lastIndex = value;
            }
        }

        public SeparationPoints(int firstIndex, int lastIndex) {
            this.firstIndex = firstIndex;
            this.lastIndex = lastIndex;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (this.destructionTags.Contains(other.gameObject.tag)) {
            this.StartCoroutine(this.waitForInsertion(other));
        }
    }

    public IEnumerator waitForInsertion(Collider2D other) {
        while (this.working) {
            yield return null;
        }
        if (other.enabled) {
            this.working = true;
            this.createPoints(other);
            this.working = false;
        }
    }

    private void createPoints(Collider2D other) {

        CircleCollider2D circle2D = other.gameObject.GetComponent<CircleCollider2D>();
        Vector2 center = other.transform.TransformPoint(circle2D.offset);
        float radius = circle2D.radius * Mathf.Max(circle2D.transform.localScale.x, circle2D.transform.localScale.y);
        PolygonCollider2D onePathCollider = this.gameObject.AddComponent<PolygonCollider2D>();
        List<List<Vector2>> newPaths = new List<List<Vector2>>();
        float minDistance = this.getMinDistance(center, radius);

        for (int i = 0; i < this.polygonCollider2D.pathCount; i++) {
            int lastExplosionInsertion = -1;
            Vector2[] oldPath = this.polygonCollider2D.GetPath(i);
            List<Vector2> newPath = new List<Vector2>();
            List<SeparationPoints> pointsNewPaths = new List<SeparationPoints>();

            onePathCollider.SetPath(0, oldPath);
            for (int j = 0; j < oldPath.Length; j++) {
                Vector2 firstPoint = oldPath[j];
                Vector2 nextPoint = oldPath[(j + 1) % oldPath.Length];

                this.addPointIfOutsideExplosion(circle2D, firstPoint, newPath);
                int firstImpactPointIndex = this.addExplosionSurfacePointsToPath(onePathCollider, circle2D, center, radius, firstPoint, nextPoint, newPath);
                this.addNewPathPoints(newPath, firstImpactPointIndex, lastExplosionInsertion, minDistance, pointsNewPaths);
                Vector2 lastImpactPoint = this.addExplosionLastPointToPath(circle2D, firstPoint, nextPoint, newPath);
                if (lastImpactPoint != Vector2.zero) {
                    lastExplosionInsertion = newPath.Count - 1;
                }
            }
            this.createPaths(pointsNewPaths, newPath, newPaths);
        }
        Destroy(onePathCollider);
        other.enabled = false;

        this.polygonCollider2D.pathCount = newPaths.Count;
        for (int i = 0; i < newPaths.Count; i++) {
            this.polygonCollider2D.SetPath(i, newPaths[i].ToArray());
        }
    }

    private void addNewPathPoints(List<Vector2> newPath, int firstImpactPointIndex, int lastExplosionInsertion, float minDistance, List<SeparationPoints> pointsNewPaths) {
        if (newPath.Count >= 2 && firstImpactPointIndex != -1 && lastExplosionInsertion != -1 && Vector2.Distance(newPath[lastExplosionInsertion], newPath[newPath.Count - 1]) <= minDistance) {
            SeparationPoints points = new SeparationPoints(lastExplosionInsertion, newPath.Count - 1);
            pointsNewPaths.Add(points);
        }
    }

    private void createPaths(List<SeparationPoints> pointsNewPaths, List<Vector2> fullNewPath, List<List<Vector2>> newPaths) {
        int index = 0;
        List<Vector2> main = new List<Vector2>();
        foreach (SeparationPoints newPoints in pointsNewPaths) {
            List<Vector2> path = new List<Vector2>();
            main.AddRange(fullNewPath.GetRange(index, newPoints.FirstIndex - index));
            path.AddRange(fullNewPath.GetRange(newPoints.FirstIndex, newPoints.LastIndex - newPoints.FirstIndex));
            if (path.Count > 2)
                newPaths.Add(path);
            index = newPoints.LastIndex + 1;
        }
        main.AddRange(fullNewPath.GetRange(index, fullNewPath.Count - index));
        newPaths.Add(main);
    }

    private float getMinDistance(Vector2 center, float radius) {
        float angle = 0.0f;
        return Vector2.Distance(this.getNextPointOnCircle(ref angle, center, radius), this.getNextPointOnCircle(ref angle, center, radius));
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
        Vector2 firstImpactPoint = this.getCollisionPoint(firstPoint, nextPoint);
        int firstImpactPointIndex = -1;
        if (firstImpactPoint != Vector2.zero && !circle2D.OverlapPoint(this.transform.TransformPoint(firstPoint))) {
            float angle = getAngle(firstImpactPoint, center, radius);

            if (firstImpactPoint != Vector2.zero) {
                this.addWorldPointToPath(path, firstImpactPoint);
                firstImpactPointIndex = path.Count - 1;
                this.addNextExplosionPointsToPath(terrain, path, ref angle, center, radius);
            }
        }
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
        Vector2 pointWorld = this.transform.InverseTransformPoint(point);
        path.Add(pointWorld);
    }

    private void addNextExplosionPointsToPath(PolygonCollider2D terrain, List<Vector2> path, ref float angle, Vector2 center, float radius) {
        Vector2 surfacePointCircle = this.getNextPointOnCircle(ref angle, center, radius);
        while (terrain.OverlapPoint(surfacePointCircle)) {
            this.addWorldPointToPath(path, surfacePointCircle);
            surfacePointCircle = this.getNextPointOnCircle(ref angle, center, radius);
        }
    }

    private Vector2 getNextPointOnCircle(ref float angle, Vector2 center, float radius) {
        angle += this.angleDiference;
        float x = center.x + (radius) * Mathf.Cos(angle);
        float y = center.y + (radius) * Mathf.Sin(angle);
        return new Vector2(x, y);
    }

    private Vector2 getCollisionPoint(Vector2 point1, Vector2 point2) {
        RaycastHit2D[] hitInfo = Physics2D.LinecastAll(this.transform.TransformPoint(point1), this.transform.TransformPoint(point2));
        foreach (RaycastHit2D info in hitInfo) {
            if (this.destructionTags.Contains(info.collider.tag)) {
                return info.point;
            }
        }
        return Vector2.zero;
    }

    private float getAngle(Vector2 surf, Vector2 center, float radius) {
        float angle = (Mathf.Atan2(surf.y - center.y, surf.x - center.x)) % (2 * Mathf.PI);
        if (angle < 0) {
            angle += 2 * Mathf.PI;
        }
        return angle;
    }
}