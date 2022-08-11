using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    public float damage = 20f;
    public float aliveTime = 2f;
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

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessageUpwards("TakeDamage", damage);
            Debug.Log(collision.gameObject.name);
            //Destroy(gameObject);
        }
    }*/

    
}
