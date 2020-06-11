using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public Flock flock;
    public bool changeState = false;
    private void Start()
    {
        //making sure flock is assigned
        flock = GetComponent<Flock>();
        if (flock == null)
        {
            Debug.LogError("Statemachine couldn't find flock");
        }
    }
}
