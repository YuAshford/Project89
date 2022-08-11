using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided = false;

    public int Damage = 10;

    public float aliveTime = 5f;
    private float curAliveTime = 0;


    private void Update()
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

            Destroy(gameObject);
        }
    }
    /*private void OnTriggerEnter(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Target"));
        foreach (Collider c in cols)
        {
            c.SendMessageUpwards("TakeDamage", damage);
            Debug.Log("Hit");
        }
        Destroy(gameObject);
    }*/
}
