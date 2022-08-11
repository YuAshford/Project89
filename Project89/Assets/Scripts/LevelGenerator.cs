using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public static int[] leftWingRooms;
    public static int[] frontWingRooms;
    public static int[] rightWingRooms;

    public bool[][] needEnemy;

    private int allScenesCount;
    private int allCapScenesCount = 5;

    private int rand;
    // Start is called before the first frame update
    void Awake()
    {
        leftWingRooms = new int[3];
        frontWingRooms = new int[3];
        rightWingRooms = new int[3];

        needEnemy = new bool[3][];
        needEnemy[0] = new bool[3]; //Left
        needEnemy[1] = new bool[3]; //Right
        needEnemy[2] = new bool[3]; //Front

        allScenesCount = SceneManager.sceneCountInBuildSettings;
        if (!LevelManager.isGenerated)
        {
            Generate();

            LevelManager.leftWing = leftWingRooms;
            LevelManager.rightWing = rightWingRooms;
            LevelManager.frontWing = frontWingRooms;
            LevelManager.isGenerated = true;

            UpdateEnemySpawners();
        }
    }

    private void Generate()
    {
        for (var depth = 0; depth < 3; depth++)
        {
            if (depth < 2)
            {
                rand = Random.Range(2, allScenesCount - allCapScenesCount); // was 1
                leftWingRooms[depth] = rand;
                Debug.Log(rand);
                rand = Random.Range(2, allScenesCount - allCapScenesCount);
                frontWingRooms[depth] = rand;
                Debug.Log(rand);
                rand = Random.Range(2, allScenesCount - allCapScenesCount);
                rightWingRooms[depth] = rand;
                Debug.Log(rand);
            }
            else
            {
                rand = Random.Range(allScenesCount - allCapScenesCount, allScenesCount - 1);
                leftWingRooms[depth] = rand;
                Debug.Log(rand);
                //rand = Random.Range(allScenesCount - allCapScenesCount, allScenesCount);
                frontWingRooms[depth] = allScenesCount - 1;
                Debug.Log(rand);
                rand = Random.Range(allScenesCount - allCapScenesCount, allScenesCount - 1);
                rightWingRooms[depth] = rand;
                Debug.Log(rand);
            }
        }
    }

    private void UpdateEnemySpawners()
    {
        foreach (var wingArray in needEnemy)
        {
            for (int i = 0; i < wingArray.Length; i++)
                wingArray[i] = true;
        }

        LevelManager.needEnemyLeftWing = needEnemy[0];
        LevelManager.needEnemyRightWing = needEnemy[1];
        LevelManager.needEnemyFrontWing = needEnemy[2];
    }

}
