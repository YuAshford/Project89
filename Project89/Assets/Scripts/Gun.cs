using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public LineRenderer lineRenderer;

    private Transform firePoint;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        firePoint = transform.Find("firePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            
            Shoot();
        }
        //lineRenderer.enabled = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, Vector3.zero);
            var target = hit.transform.GetComponent<Drone>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
