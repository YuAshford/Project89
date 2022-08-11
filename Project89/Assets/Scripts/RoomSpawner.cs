using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top
    // 3 --> need left
    // 4 --> need right
    private PlayerMovement levelManager;

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;
    private int maxRooms = 3;
    private int currentRoomsCount;

    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    private void Start()
    {
        currentRoomsCount = Global.currentRoomsCount;
        Debug.Log(currentRoomsCount);
        Invoke("Spawn", 0.3f);

    }

    private void Spawn()
    {
        if (!spawned)
        {
            currentRoomsCount++;
            if (openingDirection == 1)
            {
                if (currentRoomsCount == 3)
                {
                    RoomGenerate(templates.capBottomRooms);
                    currentRoomsCount = 0;
                }
                else
                    RoomGenerate(templates.bottomRooms);
            }
            else if (openingDirection == 2)
            {
                if (currentRoomsCount == 3)
                {
                    RoomGenerate(templates.capTopRooms);
                    currentRoomsCount = 0;
                }
                else
                    RoomGenerate(templates.topRooms);
            }
            else if (openingDirection == 3)
            {
                if (currentRoomsCount == 3)
                {
                    RoomGenerate(templates.capLeftRooms);
                    currentRoomsCount = 0;
                }
                else
                    RoomGenerate(templates.leftRooms);
            }
            else if (openingDirection == 4)
            {
                if (currentRoomsCount == 3)
                {
                    RoomGenerate(templates.capRightRooms);
                    currentRoomsCount = 0;
                }
                else
                    RoomGenerate(templates.rightRooms);
            }
        }
        spawned = true;
        Global.currentRoomsCount = currentRoomsCount;
        
    }

    private void RoomGenerate(GameObject[] roomArray)
    {
        rand = Random.Range(0, roomArray.Length);
        Instantiate(roomArray[rand], transform.position, roomArray[rand].transform.rotation);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
            Destroy(gameObject);
    }
}
