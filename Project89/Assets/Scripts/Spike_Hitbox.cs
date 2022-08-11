using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Hitbox : MonoBehaviour
{
    public int Damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.GetComponent<EnemyAI>() != null)
                other.GetComponent<EnemyAI>().TakeDamage(Damage);
            if (other.GetComponent<EnemyAIMelee>() != null)
                other.GetComponent<EnemyAIMelee>().TakeDamage(Damage);
            if (other.GetComponent<EnemyAISpecial>() != null)
                other.GetComponent<EnemyAISpecial>().TakeDamage(Damage);
        }
    }
}
