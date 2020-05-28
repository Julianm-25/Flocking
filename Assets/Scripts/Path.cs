using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints;

    public float radius;
    public float returnPercent = 0.9f;

    public Vector3 gizmoSize = Vector3.one;

    private void OnDrawGizmos()
    {
        if(waypoints == null || waypoints.Count == 0)
        {
            return;
        }
    for (int index = 0; index < waypoints.Count; index++)
        {
            Transform waypoint = waypoints[index];
            if(waypoint == null)
            {
                continue;
            }
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(waypoint.position, gizmoSize);

            if(index + 1 < waypoints.Count && waypoints[index +1] != null)
            {
                Gizmos.DrawLine(waypoint.position, waypoints[index + 1].position);
            }
        }
    }
}
