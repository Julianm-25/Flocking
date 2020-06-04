using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavior/Pursuit")]
public class PursuitBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, List<Transform> areaContext, Flock flock)
    {
        List<Transform> areaFilteredContext = (filter == null) ? areaContext : filter.Filter(agent, areaContext);
        if (areaFilteredContext.Count == 0)
        {
            return Vector2.zero;
        }
        Vector2 move = Vector2.zero;
        foreach (Transform item in areaFilteredContext)
        {
            float distance = Vector2.Distance(item.position, agent.transform.position);
            float distancePercent = distance / flock.areaRadius;
            float inverseDistancePercent = 1 - distancePercent;
            float weight = inverseDistancePercent / flock.agentsCount; // come back to this
            Vector2 direction = (item.position - agent.transform.position) * weight;
            if (direction.SqrMagnitude() > weight * weight)
            {
                direction.Normalize();
                direction *= weight;
            }
            move += direction * weight;
        }
        return move;
    }
}