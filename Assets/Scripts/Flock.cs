using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;//
    public List<FlockAgent> agents = new List<FlockAgent>();//
    public FlockBehavior behavior;
    [Range(10, 500)]
    public int startingCount = 250;//
    const float AgentDensity = 0.08f;//
    [Range(1f, 100f)]
    public float driveFactor = 10f;//Speed
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(1f, 100f)]
    public float areaRadius = 20f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;//multiplier
    float squareMaxSpeed;
    float squareNeighborRadius;
    public int agentsCount { get { return agents.Count; } }
    float squareAvoidanceRadius;//radius
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    public List<Transform> context;
    public List<Transform> areaContext;
    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;//0.5 x bigger then neighbor radius
                                                                                                             // otherSquareAvoidanceRadius = squareNeighborRadius * otherAvoidanceRadiusMultiplier * otherAvoidanceRadiusMultiplier;
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                ((Vector2)transform.position) + Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.Initialize(this);
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    private void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            context = GetNearbyObjects(agent, neighborRadius);
            areaContext = GetNearbyObjects(agent, areaRadius);
            Vector2 move = behavior.CalculateMove(agent, context, areaContext, this);
            move *= driveFactor;

            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }
    List<Transform> GetNearbyObjects(FlockAgent agent, float radius)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, radius);
        foreach(Collider2D contextCollider in contextColliders)
        {
            if(contextCollider != agent.AgentCollider)
            {
                context.Add(contextCollider.transform);
            }
        }
        return context;
    }
}