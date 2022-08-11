using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float dashEnergyCap = 9f;
    public float regenerationRate = 1f;
    public float oneDashCost = 3f;
    public float dashSpeed = 36f;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerManager.energyForDash - oneDashCost >= 0f) //dashEnergyCap - oneDashCost
        {
            Dash();
        }
        DashRegeneration();
    }

    void Dash()
    {
        playerMovement.isDashing = true;
        //dashEnergyCap -= oneDashCost;
        PlayerManager.EnergyForDashLoss(oneDashCost);
    }

    void DashRegeneration()
    {
        //dashEnergyCap += regenerationRate * Time.deltaTime;
        PlayerManager.EnergyForDashUp(regenerationRate);
        //dashEnergyCap = Mathf.Clamp(dashEnergyCap, 0f, 9f);
    }
}
