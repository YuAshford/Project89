using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 50f;
    public float damageTime = 0.5f;

    public Renderer thisRenderer;

    bool isDamaged = false;

    public void TakeDamage(float amount)
    {
        health -= amount;
        isDamaged = true;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        thisRenderer = gameObject.GetComponent<Renderer>();
    }

    void ChangeColor(Color newColor)
    {
        thisRenderer.material.SetColor("_Color", newColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamaged)
        {
            ChangeColor(Color.red);
            damageTime -= Time.deltaTime;
        }

        if (damageTime <= 0)
        {
            isDamaged = false;
            damageTime = 0.5f;
            ChangeColor(Color.white);
        }
    }
}
