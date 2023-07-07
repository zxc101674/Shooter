using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Patrol {
    public bool isMoveByCicle;
    public float minDistanceToNextPath;
    public float stopDistanceToTarget;
    public List<Vector2> pathPoints;

    private int indexPathPoint;

    public Vector2 PathPoint =>
        indexPathPoint < pathPoints.Count ?
        pathPoints[indexPathPoint] :
        Vector2.zero;
    
    public void NextPathPoint() {
        if (indexPathPoint < pathPoints.Count - (isMoveByCicle ? 0 : 1)) ++indexPathPoint;
        if (indexPathPoint == pathPoints.Count) indexPathPoint = 0;
    }

    public bool IsLastPoint => indexPathPoint == pathPoints.Count - 1;
}
public class Mono_Patrol : BaseMonoComponent<Patrol> { }