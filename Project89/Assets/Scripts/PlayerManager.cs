using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    // Start is called before the first frame update
    public static float health = 100f;
    public static float energy = 100f;
    public static float energyForDash = 9f;
    public static int keyToOpenFront = 0;
    public static bool PlayerIsDead = false;
    //public static bool inEnergyRadius = false;

    public static void TakeDamage(float damage)
    {
        if (health - damage <= 0)
            Death();
        health = Mathf.Clamp(health - damage, 0, 100);
        Debug.Log(health);
    }

    public static void EnergyGain(float energyAmount)
    {
        energy = Mathf.Clamp(energy + energyAmount, 0, 100);
    }

    public static void EnergyLoss(float energyCost)
    {
        energy = Mathf.Clamp(energy - energyCost, 0, 100);
    }

    public static void Heal(int healthPoint)
    {
        health = Mathf.Clamp(health + healthPoint, 0, 100);
    }

    public static void EnergyForDashLoss(float energyForDashCost)
    {
        energyForDash = Mathf.Clamp(energyForDash - energyForDashCost, 0, 9);
    }

    public static void EnergyForDashUp(float energyForDashUp)
    {
        energyForDash = Mathf.Clamp(energyForDash + energyForDashUp * Time.deltaTime, 0, 9);
    }

    public static void GainKey(int key)
    {
        keyToOpenFront += key;
    }

    public static void ResetKeys()
    {
        keyToOpenFront = 0;
    }

    private static void Death()
    {
        PlayerIsDead = true;
    }

    public static void ResetHealthAndEnergy()
    {
        PlayerIsDead = false;
        health = 100f;
        energy = 100f;
    }
}
