using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    public int healPerSec = 2;
    public float lifeTime = 5f;
    public float timeBetweenHeal = 0.6f;
    private bool isInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime >= 0)
            lifeTime -= Time.deltaTime;
        else
            DestroyArea();

        if (isInArea && timeBetweenHeal > 0.5f)
        {
            PlayerManager.Heal(healPerSec);
            timeBetweenHeal = 0f;
        }
        timeBetweenHeal += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInArea = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInArea = false;

        }
    }

    private void DestroyArea()
    {
        Destroy(gameObject);
    }
}
