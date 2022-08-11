using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage;
    public float lifeTime = 0.25f;
    private float timer;

    void Start()
    {
        timer = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            gameObject.SetActive(false);
            timer = lifeTime;
        }
        else
            timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.TakeDamage(Damage);
            gameObject.SetActive(false);
        }
    }
}
