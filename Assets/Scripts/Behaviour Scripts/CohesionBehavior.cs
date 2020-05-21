using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        //if no neighbors, stay still
        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }
        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in filteredContext)
        {

            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= filteredContext.Count;
        //cohesionMove = cohesionMove / context.Count;
        //create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;


        return cohesionMove;
    }
}
