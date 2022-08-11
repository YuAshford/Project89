using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloseExplousion : MonoBehaviour
{
    public int Damage = 15;
    // Start is called before the first frame update
    public float aliveTime = 0.5f;
    private float curAliveTime = 0;
    // Start is called before the first frame update
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
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.TakeDamage(Damage);
        }
    }
}
