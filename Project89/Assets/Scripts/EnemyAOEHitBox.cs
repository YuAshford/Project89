using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAOEHitBox : MonoBehaviour
{
    public int Damage;
    public float damageInTime = 0.25f;
    private float timer;
    private bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        timer = damageInTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            isHit = true;
            timer = damageInTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isHit)
            {
                PlayerManager.TakeDamage(Damage);
                isHit = false;
            }
            //gameObject.SetActive(false);
            
        }
    }
}
