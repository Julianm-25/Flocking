using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreyStateMachine : Life
{
    public enum State
    {
        Wander,
        Hide
    }
    public State state;
    [SerializeField] private FlockBehavior wanderBehavior = null;
    [SerializeField] private FlockBehavior HideBehavior = null;
    [SerializeField] private ContextFilter filter = null;

    //Moves freely as a flock
    IEnumerator WanderState()
    {
        Debug.Log("Wander Enter");
        if (flock.behavior != wanderBehavior)
        {
            flock.behavior = wanderBehavior;
        }
        while (state == State.Wander)
        {
            foreach (FlockAgent agent in flock.agents)
            {
                List<Transform> filteredContext = (filter == null) ? flock.areaContext : filter.Filter(agent, flock.areaContext);
                if (filteredContext.Count > 0)
                {
                    state = State.Hide;
                }
            }
            yield return 0;
        }
        Debug.Log("Wander Exit");
        NextState();
    }
    //Hides from predators and attemps to put an object between them
    IEnumerator HideState()
    {
        if (flock.behavior != HideBehavior)
        {
            flock.behavior = HideBehavior;
        }
        Debug.Log("Hide Enter");
        while (state == State.Hide)
        {
            foreach (FlockAgent agent in flock.agents)
            {
                List<Transform> filteredContext = (filter == null) ? flock.areaContext : filter.Filter(agent, flock.areaContext);
                if (filteredContext.Count == 0)
                {
                    state = State.Wander;
                }
            }
            yield return 0;
        }
        Debug.Log("Hide Exit");
        NextState();
    }
    public void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
    //assigns a state on start
    private void Start()
    {
        NextState();
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