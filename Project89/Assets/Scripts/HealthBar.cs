using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        health = PlayerManager.health;
        
    }

    // Update is called once per frame
    void Update()
    {
        health = PlayerManager.health;
        healthText.text = "HEALTH: " + health;
    }
}
