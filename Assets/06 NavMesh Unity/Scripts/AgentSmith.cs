using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentSmith : MonoBehaviour
{
    private WavePoint [] waypoints;
    private NavMeshAgent agent;

    // Gives us a random waypoint everytime we access the variable
    private WavePoint RandomWaypoint => waypoints[Random.Range(0, waypoints.Length)];
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        waypoints = FindObjectsOfType<WavePoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(RandomWaypoint.Position);
        }
    }
}
