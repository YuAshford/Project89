using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Text energyText;
    private float energy;
    // Start is called before the first frame update
    private void Start()
    {
        energy = PlayerManager.energy;
    }


    // Update is called once per frame
    void Update()
    {
        energy = PlayerManager.energy;
        energyText.text = "ENERGY: " + energy;
    }
}
