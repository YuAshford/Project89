using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemies;
    private GameObject[] spawnLocations;
    private float time = 3;
    private bool flag = true;
    private bool isEnemySpawned = false;
    private bool stopFalsing = false;
    private int enemyCount;

    private void Awake()
    {

    }

    void Start()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPointEnemy");
        //SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (var e in spawnLocations)
        {
            int randIndex = Random.Range(0, 3);

            GameObject.Instantiate(enemies[randIndex], e.transform.position, Quaternion.identity); //[randIndex]
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
            
        }

        //Debug
        

        //Debug

        else if (LevelManager.currentWing == "Left")
        {
            if (flag && LevelManager.needEnemyLeftWing[LevelManager.currentRoomIndex])
            {
                Debug.Log("Spawn");
                flag = false;
                SpawnEnemies();
                isEnemySpawned = true;
            }
            
        }
        else if (LevelManager.currentWing == "Right")
        {
            if (flag && LevelManager.needEnemyRightWing[LevelManager.currentRoomIndex])
            {
                Debug.Log("Spawn");
                flag = false;
                SpawnEnemies();
                isEnemySpawned = true;
            }
        }
        else if (LevelManager.currentWing == "Front")
        {
            if (flag && LevelManager.needEnemyFrontWing[LevelManager.currentRoomIndex])
            {
                Debug.Log("Spawn");
                flag = false;
                SpawnEnemies();
                isEnemySpawned = true;
            }
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && isEnemySpawned && !stopFalsing)
        {
            Debug.Log("Ha-haah");
            //open the door

            if (LevelManager.currentWing == "Left")
                LevelManager.needEnemyLeftWing[LevelManager.currentRoomIndex] = false;
            else if (LevelManager.currentWing == "Right")
                LevelManager.needEnemyRightWing[LevelManager.currentRoomIndex] = false;
            else if (LevelManager.currentWing == "Front")
                LevelManager.needEnemyFrontWing[LevelManager.currentRoomIndex] = false;

            stopFalsing = true;
        }
    }
}
