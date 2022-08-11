using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttack : MonoBehaviour
{
    public Camera cam;
    public GameObject spike;
    public Transform firePoint;
    public float fireRate = 0.5f;
    public float energyCost = 20f;
    
    public Collider col;

    private float timeToFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Time.time >= timeToFire && PlayerMovement.isGrounded && PlayerManager.energy >= energyCost)
        {
            //spike = Instantiate(spike, firePoint.position, Quaternion.identity);
            //col = spike.gameObject.transform.Find("spike_hitbox").GetComponent<MeshCollider>();
            PlayerManager.EnergyLoss(energyCost);
            timeToFire = Time.time + 1 / fireRate;
            LaunchAttack();
        }
    }

    void LaunchAttack()
    {
        Instantiate(spike, firePoint.position, Quaternion.LookRotation(cam.transform.right * -1, Vector3.up));
    }
}
