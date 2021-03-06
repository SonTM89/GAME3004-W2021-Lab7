using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public enum ZombieState
{
    IDLE,
    RUN,
    JUMP,
    PUNCH,
    DIE
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public PlayerBehaviour player;
    public Animator controller;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerBehaviour>();
        controller = GetComponent<Animator>();

        if(controller)
        {
            controller.SetInteger("AnimState", (int)ZombieState.RUN);
        }          
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            navMeshAgent.SetDestination(player.transform.position);

            var distance = Vector3.Distance(player.transform.position, transform.position);

            // if the enemy is close enough to the player, perform an attack
            if(controller && distance<= 3.0f)
            {
                //var direction = Vector3.Normalize(transform.position - player.transform.position);

                //transform.LookAt(player.transform.position - new Vector3(0.0f, 0.5f, 0.0f));

                controller.SetInteger("AnimState", (int)ZombieState.PUNCH);
            }
        }        
    }
}