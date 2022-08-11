using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAISpecial : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform firePointLaser, firePointFissure;

    public RaycastHit rayHit;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public float range;
    public float ShootingTime;
    public float limitShootingTime;
    public float reloadLaserTime = 10f;
    public float afterFireTime;
    public float Damage;
    public ParticleSystem flame;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject spawnedLaser; //laserPrefab
    //private GameObject spawnedLaser;

    public float sightRange, attackRange, shootRange;
    public bool playerInSightRange, playerInAttackRange, playerInShootingRange, shootingIsAvailable, isShootingLaser, isHorizontal;
    public GameObject healArea;
    public GameObject HitBoxMeleeVertical, HitBoxMeleeHorizontal;

    private void Awake()
    {
        //player = GameObject.Find("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        HitBoxMeleeVertical.SetActive(false);
        HitBoxMeleeHorizontal.SetActive(false);
        firePointLaser.GetChild(0).gameObject.SetActive(false);
    }

    private void Start()
    {
       // spawnedLaser = Instantiate(laserPrefab, firePointLaser);
        
        shootingIsAvailable = true;
        isHorizontal = true;
    }

    // Start is called before the first frame update
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInShootingRange = Physics.CheckSphere(transform.position, shootRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange && !isShootingLaser) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !isShootingLaser)
        { 
            AttackPlayer();
        }
        if (!playerInAttackRange && playerInShootingRange && shootingIsAvailable)
        {
            if (limitShootingTime >= ShootingTime)
            {
                spawnedLaser.SetActive(true);
                isShootingLaser = true;
                ShootingTime += Time.deltaTime;
                ShootPlayer();
            }
            else
            {
                isShootingLaser = false;
                ResetLaserAttack();
                ShootingTime = 0;
            }
        }
        else if (isShootingLaser && shootingIsAvailable)
        {
            if (limitShootingTime >= ShootingTime)
            {
                spawnedLaser.SetActive(true);
                isShootingLaser = true;
                ShootingTime += Time.deltaTime;
                ShootPlayer();
            }
            else
            {
                isShootingLaser = false;
                ResetLaserAttack();
                ShootingTime = 0;
            }
        }


        if (reloadLaserTime > afterFireTime && !shootingIsAvailable)
            afterFireTime += Time.deltaTime;
        else if (!shootingIsAvailable)
        {
            shootingIsAvailable = true;
        }


        //Debug.Log(playerInAttackRange);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        transform.LookAt(target); //Залочить по Y оси
        transform.GetChild(1).LookAt(player);

        if (!alreadyAttacked)
        {
            if (isHorizontal)
            {
                HitBoxMeleeHorizontal.SetActive(true);
                isHorizontal = false;
            }
            else
            {
                HitBoxMeleeVertical.SetActive(true);
                isHorizontal = true;
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ShootPlayer()
    {
       // flame.Play();
        
        agent.SetDestination(transform.position);

        Vector3 target = new Vector3(player.gameObject.GetComponent<PlayerMovement>().PlayerOldPosition.position.x, transform.position.y,
            player.gameObject.GetComponent<PlayerMovement>().PlayerOldPosition.position.z);

        Vector3 direction = firePointLaser.forward;

        transform.LookAt(target); //Залочить по Y оси
        transform.GetChild(1).LookAt(player.gameObject.GetComponent<PlayerMovement>().PlayerOldPosition);

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        HitBoxMeleeHorizontal.SetActive(false);
        HitBoxMeleeVertical.SetActive(false);
    }

    private void ResetLaserAttack()
    {
        spawnedLaser.SetActive(false);
        shootingIsAvailable = false;
        afterFireTime = 0;
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
