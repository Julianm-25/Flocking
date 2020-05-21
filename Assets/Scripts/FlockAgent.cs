﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    private Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        if (agentCollider == null)
        {
            Debug.LogError("FlockAgent can't find Collider2D");
            return;
        }
    }
    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}