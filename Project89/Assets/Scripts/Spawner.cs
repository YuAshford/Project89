using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject[] spawnLocations;
    public GameObject player;
    private GameObject[] spawnLocations;
    private void Awake()
    {

    }

    void Start()
    {
        player = (GameObject)Resources.Load("Player", typeof(GameObject));
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        foreach(var e in spawnLocations)
        {
            Debug.Log("-------SPAWN---------");
            Debug.Log(e.GetComponent<SpawnPoint>().spawnerId);
            Debug.Log(LevelManager.previousRoomIndex);

            if (LevelManager.previousRoomIndex == e.GetComponent<SpawnPoint>().spawnerId)
            {
                Debug.Log("True");
                GameObject.Instantiate(player, e.transform.position, e.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
