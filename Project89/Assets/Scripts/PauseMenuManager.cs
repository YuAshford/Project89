using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu, pCam;
    public bool pauseMenuActive;
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        pCam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB");
            pauseMenuActive = !pauseMenuActive;

            if (pauseMenuActive)
            {
                isPaused = true;
                PauseMenuOpen();
            }

            if (!pauseMenuActive)
            {
                isPaused = false;
                PauseMenuClose();
            }
        }
    }

    void PauseMenuOpen()
    {
        for (var i = 1; i < 7; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        pCam.GetComponent<MouseLook>().enabled = false;
    }

    public void PauseMenuClose()
    {
        for (var i = 1; i < 7; i++)
        {
            if (i == 2) continue;
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuActive = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        pCam.GetComponent<MouseLook>().enabled = true;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
