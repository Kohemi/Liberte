using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleMove : MonoBehaviour
{
    public bool move;
    public Transform target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move) agent.SetDestination(target.position);
    }
}