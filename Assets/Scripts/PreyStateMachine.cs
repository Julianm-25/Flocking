using UnityEngine;
using System.Collections;

public class PreyStateMachine : MonoBehaviour
{
    public enum State
    {
        Crawl,
        Walk,
        Die,
        Another,
    }

    public State state;
    public bool ChangeState = false;


    IEnumerator AnotherState()
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
    }

    private void Start()
    {
        NextState();
    }

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                   System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)info.Invoke(this,null));
    }
}