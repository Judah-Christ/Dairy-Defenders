using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLevelChange : MonoBehaviour
{

    private NavMeshAgent agent;
    private bool isUsingLadder = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (agent.isOnOffMeshLink && !isUsingLadder)
        {
            isUsingLadder = true;
            OnStartUsingLadder();
        }
        else if (!agent.isOnOffMeshLink && isUsingLadder)
        {
            isUsingLadder = false;
            OnEndUsingLadder();
        }
    }

    private void OnStartUsingLadder()
    {

    }

    private void OnEndUsingLadder()
    {
        
    }
}
