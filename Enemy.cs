using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask deadLayer;
    [SerializeField] private float maxHealth = 20f;
    private float currentHealth;
    public bool amIDead;
    public GameObject chick;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        

        currentHealth -= damage;
        animator.SetTrigger("Hurt");       
        if (currentHealth <= 0f)
        {
            Dead();
        }
        else
        {
            amIDead = false;
        }
    }

    void Dead()
    {
        amIDead = true;
        //Dead animation
        animator.SetTrigger("Dead");
        //Disable Collider
        GetComponent<BoxCollider>().enabled = false;
        //disable rb
        GetComponent<Rigidbody>().isKinematic = true;
        //Change Layer
        gameObject.layer = LayerMask.NameToLayer("Dead");
        //Disable Script
        GetComponent<Enemy>().enabled = false;
    }
}
