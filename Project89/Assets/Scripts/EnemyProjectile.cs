using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage;
    private float lifeTime = 4f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime >= 0)
            lifeTime -= Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (LayerMask.LayerToName(other.transform.gameObject.layer) == "Ground")
            Destroy(gameObject);
    }
}
