using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{

    public int Damage;
    public int bulletsPerTap;
    private int bulletsShot;
    public float timeBetweenShooting, spread, range, timeBetweenShots;
    public bool allowButtonHold;

    bool shooting, readyToShoot;

    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public GameObject bulletHoleGraphic;
    public GameObject bulletEnemyHit;
    public ParticleSystem muzzleFlash;
    public ParticleSystem muzzleFlash2;
    AudioSource gun_shootingSound;
    public GameObject soundPlayer;
    private GameObject temporarlySoundPlayer;
    public float limitBetweenSound = 0.2f;
    private float currentTimeBetweenSound = 0;

    private void ShootInput()
    {
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0); //Down

        if (readyToShoot && shooting)
        {
            bulletsShot = bulletsPerTap;
            //gun_shootingSound.Stop();
            Shoot();

        }
    }

    private void PlaySound()
    {
        temporarlySoundPlayer = Instantiate(soundPlayer);
        temporarlySoundPlayer.GetComponent<AudioSource>().Play();
    }

    private void Shoot()
    {
        readyToShoot = false;
        muzzleFlash.Play();
        
        if (muzzleFlash2 != null)
            muzzleFlash2.Play();
        if (soundPlayer != null)
        {
            //Invoke("PlaySound", 0.6f);
            temporarlySoundPlayer = Instantiate(soundPlayer);
            temporarlySoundPlayer.GetComponent<AudioSource>().Play();
        }
        else
        {
            gun_shootingSound.Play();
        }
        //gun_shootingSound.Play();
        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction With Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            //Debug.Log(rayHit.collider.name);

            var target = rayHit.transform.GetComponent<EnemyAI>(); //Drone
            if (target != null)
            {
                Instantiate(bulletEnemyHit, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                target.TakeDamage(Damage);
            }
            else if (LayerMask.LayerToName(rayHit.transform.gameObject.layer) == "Ground")
            {
                Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            }
            if (rayHit.collider.CompareTag("Enemy"))
            {
               // Debug.Log("HIT");
                if (rayHit.collider.GetComponent<EnemyAI>() != null)
                    rayHit.collider.GetComponent<EnemyAI>().TakeDamage(Damage);
                if (rayHit.collider.GetComponent<EnemyAIMelee>() != null)
                    rayHit.collider.GetComponent<EnemyAIMelee>().TakeDamage(Damage);
                if(rayHit.collider.GetComponent<EnemyAISpecial>() != null)
                    rayHit.collider.GetComponent<EnemyAISpecial>().TakeDamage(Damage);
            }
        }

        
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        Invoke("ResetShot", timeBetweenShooting);

        bulletsShot -= 1;
        if (bulletsShot > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        
    }

    private void Awake()
    {
        readyToShoot = true;
    }

    void Start()
    {
        gun_shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuManager.isPaused)
        {
            shooting = false;
            ShootInput();
        }
    }
}
