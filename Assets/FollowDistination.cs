using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowDistination : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavMeshAgentObject;
    public Transform Destination;
    void Start()
    {
        NavMeshAgentObject = gameObject.GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        NavMeshAgentObject.SetDestination(Destination.transform.position);
    }
}
