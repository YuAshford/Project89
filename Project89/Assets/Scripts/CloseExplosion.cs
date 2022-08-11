using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseExplosion : MonoBehaviour
{
    public int Damage = 30;
    // Start is called before the first frame update
    public float aliveTime = 0.5f;
    private float curAliveTime = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curAliveTime < aliveTime)
        {
            curAliveTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
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
