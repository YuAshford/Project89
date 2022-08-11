using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public OnTriggerLoadLevel trigger;
    public int spawnerId;
    private bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        spawnerId = trigger.nextRoomInd;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (firstTime)
        {
            spawnerId = trigger.nextRoomInd;
            firstTime = false;
        }*/
    }
}
