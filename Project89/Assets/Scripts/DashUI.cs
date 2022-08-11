using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    public Text dashText;
    private int dashCount;
    // Start is called before the first frame update
    void Start()
    {
        dashCount = (int)(PlayerManager.energyForDash / 3f);
    }

    // Update is called once per frame
    void Update()
    {
        dashCount = (int)(PlayerManager.energyForDash / 3f);
        dashText.text = "DASH >> " + dashCount;
    }
}
