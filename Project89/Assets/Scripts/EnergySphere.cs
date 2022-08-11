using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphere : MonoBehaviour
{
    public float energyRegenerationRate = 1f;
    public float energyRegenerateTime = 0.05f;
    private float currentEnergyRegenerateTime = 1f;
    public bool inEnergyRadius = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inEnergyRadius && currentEnergyRegenerateTime > energyRegenerateTime)
        {
            PlayerManager.EnergyGain(energyRegenerationRate);
            currentEnergyRegenerateTime = 0;
        }
        else if (currentEnergyRegenerateTime < energyRegenerateTime)
        {
            currentEnergyRegenerateTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Target"));

        //if (cols.Length != 0)
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy in area");
            inEnergyRadius = true;
        }
        //else
          //  inEnergyRadius = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            inEnergyRadius = false;
    }
}
