using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, maintain current alignment
        if (context.Count == 0)
        {
            return agent.transform.up;
        }
        //add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int AvoidCount = 0;
        foreach (Transform item in context)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                AvoidCount++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }

        if (AvoidCount > 0)
        {
            avoidanceMove /= AvoidCount;
        }
        return avoidanceMove;
    }
}