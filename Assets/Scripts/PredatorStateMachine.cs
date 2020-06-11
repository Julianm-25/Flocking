using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Flock))]
public class PredatorStateMachine : Life
{
    public enum State
    {
        Patrol,
        Chase
    }
    public float timer;
    public State state;
    [SerializeField] private FlockBehavior patrolBehavior = null;
    [SerializeField] private FlockBehavior chaseBehavior = null;
    [SerializeField] private ContextFilter filter = null;

    //Moves between waypoints
    IEnumerator PatrolState()
    {
        Debug.Log("Patrol Enter");
        if (flock.behavior != patrolBehavior)
        {
            flock.behavior = patrolBehavior;
        }
        while (state == State.Patrol)
        {
            timer += Time.deltaTime;
            foreach(FlockAgent agent in flock.agents)
            {
                List<Transform> filteredContext = (filter == null) ? flock.areaContext : filter.Filter(agent, flock.areaContext);
                if (filteredContext.Count > 0 && timer >= 15)
                {
                    state = State.Chase;
                    timer = 0;
                }
            }
            yield return 0;
        }
        Debug.Log("Patrol Exit");
        NextState();
    }
    //Follows prey
    IEnumerator ChaseState()
    {
        if (flock.behavior != chaseBehavior)
        {
            flock.behavior = chaseBehavior;
        }
        Debug.Log("Chase Enter");
        while (state == State.Chase)
        {
            timer += Time.deltaTime;
            if (timer >= 15)
            {
                state = State.Patrol;
                timer = 0;
            }
            yield return 0;
        }
        Debug.Log("Chase Exit");
        NextState();
    }
    private void Start()
    {
        NextState();
    }
    public void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
    /*IEnumerator AnotherState()
    {
        Debug.Log("Another: Enter");
        while (state == State.Another)
        {
            //WE CAN WRITE SOME CODE HERE! MAYBE AI CODE???
            //MoveAI();
            yield return 0;
        }
        Debug.Log("Another: Exit");
        NextState();
    }


    IEnumerator CrawlState()
    {
        Debug.Log("Crawl: Enter");

        while (state == State.Crawl)//looping
        {
            //ChasingPlayer();
            if(ChangeState == true)
            {
                state = State.Walk; //changing states
            }

            yield return 0;
        }


        Debug.Log("Crawl: Exit");
        NextState();
    }



    IEnumerator WalkState()
    {
        Debug.Log("Walk: Enter");

        while (state == State.Walk)
        {
            if (ChangeState == false)
            {
                state = State.Crawl; //changing states
            }
            yield return 0;
        }
        Debug.Log("Walk: Exit");
        NextState();
    }



    IEnumerator DieState()
    {
        Debug.Log("Die: Enter");
        while (state == State.Die)
        {
            yield return 0;
        }
        Debug.Log("Die: Exit");
    }*/
}