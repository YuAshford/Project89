using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float health = 50f;
    public float damageTime = 0.5f;
    public float damage = 10f;
    public float timeVisible = 0.3f;
    public float attackRate = 1f;
    public float currentAttackRate = 1.5f;
    private Renderer thisRenderer;
    public Collider attackHitBox;
    private bool isAttacking = false;
    private Renderer hitboxRenderer;
    

    bool isDamaged = false;

    [SerializeField] private LayerMask _layerMask;

    private float _attackRange = 3f;
    private float _rayDistance = 5.0f;
    private float _stoppingDistance = 5f;

    private Vector3 _destination;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private PlayerMovement _target;
    private DroneState _currentState;
    // Start is called before the first frame update
    void Start()
    {
        thisRenderer = gameObject.GetComponent<Renderer>();
        //var targetToAggro = CheckForAggro();
        _target = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        _currentState = DroneState.Chase;
        Debug.Log(_currentState);
        Debug.Log(_target.name);
        hitboxRenderer = transform.Find("DroneHitbox").GetComponent<Renderer>();
        hitboxRenderer.enabled = false;

        /*
        if (NeedsDestination())
            GetDestination();
        transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        */
    }

    // Update is called once per frame
    void Update()
    {
        currentAttackRate += Time.deltaTime;

        if (isDamaged)
        {
            ChangeColor(Color.red);
            damageTime -= Time.deltaTime;
            if (damageTime <= 0)
            {
                isDamaged = false;
                damageTime = 0.5f;
                ChangeColor(Color.white);
            }
        }

        if (isAttacking && timeVisible > 0f)
        {
            hitboxRenderer.enabled = true;
            timeVisible -= Time.deltaTime;
        }
        else
        {
            hitboxRenderer.enabled = false;
            timeVisible = 0.3f;
            isAttacking = false;
        }

        switch (_currentState)
        {
            /*
            case DroneState.Wander:
            {
                    if (NeedsDestination())
                        GetDestination();

                    transform.rotation = _desiredRotation;

                    transform.Translate(Vector3.forward * Time.deltaTime * 5f);

                    var rayColor = IsPathBlocked() ? Color.red : Color.green;
                    Debug.DrawRay(transform.position, _direction * _rayDistance, rayColor);

                    while (IsPathBlocked())
                    {
                        Debug.Log("Path Blocked");
                        GetDestination();
                    }

                    var targetToAggro = CheckForAggro();
                    if (targetToAggro != null)
                    {
                        _target = targetToAggro.GetComponent<PlayerManager>();
                        _currentState = DroneState.Chase;
                    }

                    break;
            }
            */
            case DroneState.Chase:
            {
                   /* 
                    if (_target == null)
                    {
                        _currentState = DroneState.Wander;
                        return;
                    }
                   */
                    transform.LookAt(_target.transform);
                    

                    if (Vector3.Distance(transform.position, _target.transform.position) < _attackRange)
                    {
                        
                        transform.Translate(Vector3.zero);
                        _currentState = DroneState.Attack;
                    }
                    else
                    {
                        
                        transform.Translate(Vector3.forward * Time.deltaTime * 5f);
                    }
                    break;

            }
            case DroneState.Attack:
                {
                    if (_target != null && attackRate < currentAttackRate)
                    {
                        currentAttackRate = 0;
                        Attack(attackHitBox);
                        isAttacking = true;
                    }


                    _currentState = DroneState.Chase;
                    break;
                }
        }
    }
    /*
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, _direction);
        var hitSomething = Physics.RaycastAll(ray, _rayDistance, _layerMask);
        return hitSomething.Any();
    }
    
    private void GetDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f)) +
            new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f,
            UnityEngine.Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    private bool NeedsDestination()
    {
        if (_destination == Vector3.zero)
            return true;

        var distance = Vector3.Distance(transform.position, _destination);
        if (distance <= _stoppingDistance)
        {
            return true;
        }

        return false;
    }
    
    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggro()
    {
        float aggroRadius = 5f;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, aggroRadius))
            {
                var player = hit.collider.GetComponent<PlayerManager>();
                if (player != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return player.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);

                }
            }
            else
            {
                Debug.DrawRay(pos, direction * aggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }
        return null;
    }
    */
    public enum DroneState
    {
        Wander,
        Chase,
        Attack
    }

    private void Attack(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Player"));
        foreach (Collider c in cols)
        {
            //c.SendMessageUpwards("TakeDamage", damage);
            PlayerManager.TakeDamage(damage);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("OUF");
        isDamaged = true;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void ChangeColor(Color newColor)
    {
        thisRenderer.material.SetColor("_Color", newColor);
    }
}
