using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject target;
    private NavMeshAgent agent;
    private bool finish;

    public float Speed
    {
        get => speed;
        set
        {
            speed = value;
            agent.speed = value;
        }
    }

    void Start()
    {
        finish = false;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("ForestCastle_Blue");
        agent.speed = speed;
        agent.SetDestination(target.transform.position);
    }


    public bool isFinish()
    {
        return finish;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "ForestCastle_Blue")
        {
            finish = true;
        }
    }

    public void Stop()
    {
        agent.isStopped = true;
    }
}