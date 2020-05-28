using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Hide")]
public class HideBehavior : FilteredFlockBehavior
{
    public ContextFilter obstaclesFilter;

    public float hideBehindObstacleDistance = 2f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, List<Transform> areaContext, Flock flock)
    {
        //hide from
        List<Transform> filteredContext = (filter == null) ? areaContext : filter.Filter(agent, areaContext);
        //hide behind
        List<Transform> obstacleContext = (filter == null) ? areaContext : obstaclesFilter.Filter(agent, areaContext);

        if(filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        //find nearest obstacle
        float nearestDistance = float.MaxValue;
        Transform nearestObstacle = null;
        foreach(Transform item in obstacleContext)
        {
            float Distance = Vector2.Distance(item.position, agent.transform.position);
            if(Distance < nearestDistance)
            {
                nearestObstacle = item;
                nearestDistance = Distance;
            }
        }

        //return if no obstacle
        if(nearestObstacle == null)
        {
            return Vector2.zero;
        }
        //find best hiding spot
        Vector2 move = Vector2.zero;
        foreach(Transform item in filteredContext)
        {
            //direction from item to nearestObstacle
            Vector2 obstacleDirection = nearestObstacle.position - item.position;
            //add to that direction to get the point we need to hide behind
            obstacleDirection += obstacleDirection.normalized * hideBehindObstacleDistance;

            //get the position
            Vector2 hidePosition = ((Vector2)item.position) + obstacleDirection;

            move += hidePosition;
        }
        move /= filteredContext.Count;

        //FOR DEBUG ONLY
        Debug.DrawRay(move, Vector2.up * 3f);

        //find direction the ai wants to move in
        //ie the offset
        move -= (Vector2)agent.transform.position;

        return move;
    }
}
