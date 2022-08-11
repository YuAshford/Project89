using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadLevel : MonoBehaviour
{
    public GameObject guiObject;
    public int levelIndexToLoad;
    public string wing;
    public string direction;
    private int curRoomInd;
    public int nextRoomInd;
    private bool isButtonPressed = false;
    private bool isDoorLocked = false;

    private bool isInTrigger = false;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    void Start()
    {
        guiObject.SetActive(false);
        curRoomInd = LevelManager.currentRoomIndex;

        Debug.Log(curRoomInd);
        Debug.Log(wing);
        Debug.Log(direction);

        if (curRoomInd == -1)
        {
            if (wing == "Left" || gameObject.tag == "Left")
            {
                wing = "Left";
                levelIndexToLoad = LevelManager.leftWing[0];
                //LevelManager.currentRoomIndex = 0;
                nextRoomInd = curRoomInd + 1;
            }
            else if (wing == "Right" || gameObject.tag == "Right")
            {
                wing = "Right";
                levelIndexToLoad = LevelManager.rightWing[0];
                //LevelManager.currentRoomIndex = 0;
                nextRoomInd = curRoomInd + 1;
            }
            else if (wing == "Front" || gameObject.tag == "Front")
            {
                wing = "Front";
                levelIndexToLoad = LevelManager.frontWing[0];
                //LevelManager.currentRoomIndex = 0;
                nextRoomInd = curRoomInd + 1;
            }
        }
        else if (curRoomInd < 2)
        {
            wing = LevelManager.currentWing;

            if (wing == "Left")
            {
                if (direction == "forward")
                {
                    levelIndexToLoad = LevelManager.leftWing[curRoomInd + 1];
                    nextRoomInd = curRoomInd + 1;
                }
                else if (direction == "back")
                {
                    if (curRoomInd - 1 < 0)
                    {
                        levelIndexToLoad = 1; //стартовая комната
                        wing = "Start";
                    }
                    else
                        levelIndexToLoad = LevelManager.leftWing[curRoomInd - 1];
                    nextRoomInd = curRoomInd - 1;
                }
            }
            else if (wing == "Right")
            {
                if (direction == "forward")
                {
                    levelIndexToLoad = LevelManager.rightWing[curRoomInd + 1];
                    nextRoomInd = curRoomInd + 1;
                }
                else if (direction == "back")
                {
                    if (curRoomInd - 1 < 0)
                    {
                        levelIndexToLoad = 1; //start
                        wing = "Start";
                    }
                    else
                        levelIndexToLoad = LevelManager.rightWing[curRoomInd - 1];
                    nextRoomInd = curRoomInd - 1;
                }
            }
            else if (wing == "Front")
            {
                if (direction == "forward")
                {
                    levelIndexToLoad = LevelManager.frontWing[curRoomInd + 1];
                    nextRoomInd = curRoomInd + 1;
                }
                else if (direction == "back")
                {
                    if (curRoomInd - 1 < 0)
                    {
                        levelIndexToLoad = 1; //start
                        wing = "Start";
                    }
                    else
                        levelIndexToLoad = LevelManager.frontWing[curRoomInd - 1];
                    nextRoomInd = curRoomInd - 1;
                }
            }
        }
        else if (curRoomInd == 2)
        {
            wing = LevelManager.currentWing;

            if (wing == "Left")
            {
                levelIndexToLoad = LevelManager.leftWing[curRoomInd - 1];
                nextRoomInd = curRoomInd - 1;
            }
            else if (wing == "Right")
            {
                levelIndexToLoad = LevelManager.rightWing[curRoomInd - 1];
                nextRoomInd = curRoomInd - 1;
            }
            else if (wing == "Front")
            {
                levelIndexToLoad = LevelManager.frontWing[curRoomInd - 1];
                nextRoomInd = curRoomInd - 1;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (LevelManager.currentWing == "Left")
        {
            if (LevelManager.needEnemyLeftWing[LevelManager.currentRoomIndex])
            {
                isDoorLocked = true;
            }
            else
                isDoorLocked = false;
        }
        else if (LevelManager.currentWing == "Right")
        {
            if (LevelManager.needEnemyRightWing[LevelManager.currentRoomIndex])
            {
                isDoorLocked = true;
            }
            else
                isDoorLocked = false;
        }
        else if (LevelManager.currentWing == "Front")
        {
            if (LevelManager.needEnemyFrontWing[LevelManager.currentRoomIndex])
            {
                isDoorLocked = true;
            }
            else
                isDoorLocked = false;
        }
       

        if (other.gameObject.tag == "Player" && !isDoorLocked)
        {
            if (wing == "Front" && PlayerManager.keyToOpenFront == 2)
            {
                isInTrigger = true;
                Debug.Log("In zone");
                guiObject.SetActive(true);
            }
            else if (wing != "Front")
            {
                isInTrigger = true;
                Debug.Log("In zone");
                guiObject.SetActive(true);
            }
            //Debug.Log(guiObject.activeSelf);
            //if (guiObject.activeInHierarchy == true && Input.GetKeyDown(KeyCode.F)) //guiObject.activeInHierarchy == true
            /*
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F pressed");
                //LevelManager.currentRoomIndex = levelIndexToLoad;
                LevelManager.previousRoomIndex = LevelManager.currentRoomIndex;
                LevelManager.currentRoomIndex = nextRoomInd;
                LevelManager.currentWing = wing;
               // isButtonPressed = false;
                SceneManager.LoadScene(levelIndexToLoad);
            }
            */
        }
    }

    private void Update()
    {

        if (isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (wing == "Front" && direction == "forward" && (curRoomInd + 1 == 3))
                {
                    PlayerManager.ResetKeys();
                    LevelManager.ResetLevel();
                    SceneManager.LoadScene(1); // was 0
                }
                //Debug.Log("F pressed");
                //LevelManager.currentRoomIndex = levelIndexToLoad;
                else
                {
                    LevelManager.previousRoomIndex = LevelManager.currentRoomIndex;
                    LevelManager.currentRoomIndex = nextRoomInd;
                    LevelManager.currentWing = wing;
                    // isButtonPressed = false;
                    SceneManager.LoadScene(levelIndexToLoad);
                }
            }
        }
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            Input.
            isButtonPressed = true;
        }
        else
            isButtonPressed = false;*/
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInTrigger = false;
            guiObject.SetActive(false);
        }
    }
}
