using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject explosion;
    public float energyCost = 25f;

    AudioSource gun_shootingSound;

    // Start is called before the first frame update
    void Start()
    {
        gun_shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerManager.energy >= energyCost)
        {
            //spike = Instantiate(spike, firePoint.position, Quaternion.identity);
            //col = spike.gameObject.transform.Find("spike_hitbox").GetComponent<MeshCollider>();
            PlayerManager.EnergyLoss(energyCost);
            LaunchAttack();
        }

        
    }

    private void LaunchAttack()
    {
        gun_shootingSound.Play();
        Instantiate(explosion, firePoint.position, Quaternion.identity);
    }
}
