using System;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Spider : MonoBehaviour
{
    //Awake
    [SerializeField] private NavMeshAgent agent;
    private Animator animator;
    //Search
    [SerializeField] private float searchRadius;
    [SerializeField] private LayerMask layerMask;
    private bool enemyFound = false;
    private bool inAttackRange,alreadyAttacked;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    private void Update()
    {
        if (!enemyFound) SearchForEnemy();
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, layerMask);
        if (inAttackRange) Attack();
    }

    private void SearchForEnemy()
    {
        Collider[] Enemies = Physics.OverlapSphere(transform.position, searchRadius, layerMask);
        Random random = new Random();
        int randomInt = random.Next(Enemies.Length);
        Transform enemy = Enemies[randomInt].transform;
        agent.SetDestination(enemy.position);
        animator.Play("Move");
        transform.LookAt(enemy);
        enemyFound = true;
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            //Attack Code Here
        }
        
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
