using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public iTween.EaseType easeType;
    // Start is called before the first frame update
    void Start()
    {
        iTween.RotateTo(gameObject, iTween.Hash("y", 720, "time", 5f, "easetype", easeType));
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
