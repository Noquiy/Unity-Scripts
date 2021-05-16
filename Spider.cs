
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class SpiderKing : MonoBehaviour
{
    private float searchRadius = 500;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float timeBtwAttacks;

    [SerializeField] private Animator animator;

    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private int attackDamage = 30;
    private float currentHealth;

    private bool enemyFound = false;
    private Transform enemy;
    private bool alreadyAttacked = false;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (enemyFound == false)
        {
            SearchForEnemy();
        }

        WithinAttackRadius();
        Attack();
    }

    void SearchForEnemy()
    { 
            //Search and choose random Enemy
            Collider[] Enemies = Physics.OverlapSphere(transform.position, searchRadius, layerEnemy);
            Random random = new Random();
            int randomInt = random.Next(Enemies.Length);
            enemy = Enemies[randomInt].transform;
            enemyFound = true;
            //Go for enemy
            navAgent.SetDestination(enemy.position);
            transform.LookAt(enemy);
            animator.SetBool("Crawl", true);
        
    }

    bool WithinAttackRadius()
    {
        float distance = Vector3.Distance(transform.position, enemy.position);
        if (distance <= attackRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Attack()
    {
        if (WithinAttackRadius())
        {
            navAgent.SetDestination(transform.position);
            animator.SetBool("Crawl", false);
            if (alreadyAttacked == false)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBtwAttacks);
                //Attack Code
                animator.SetTrigger("BiteAttack");
                Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRadius, layerEnemy);
                foreach (var enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }
            }
        }
        
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Health(int damage)
    {
        currentHealth -= damage;
    }
}
