using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAIMelee : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform firePoint;
    public GameObject explosion;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject healArea;
    public GameObject HitBoxMelee;
    AudioSource gun_shootingSound;

    private void Awake()
    {
        //player = GameObject.Find("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        HitBoxMelee.SetActive(false);
    }

    private void Start()
    {
        gun_shootingSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        Debug.Log(playerInAttackRange);
    }

    private void ChasePlayer()
    {

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        transform.LookAt(target); //???????? ?? Y ???
        transform.GetChild(1).LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code
            //Rigidbody rb = Instantiate(projectile, transform.GetChild(1).position, Quaternion.identity).GetComponent<Rigidbody>();

            //rb.AddForce(transform.GetChild(1).forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //

            // HitBoxMelee.SetActive(true);
            gun_shootingSound.Play();
            Instantiate(explosion, firePoint.position, Quaternion.identity);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        //HitBoxMelee.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Instantiate(healArea, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
