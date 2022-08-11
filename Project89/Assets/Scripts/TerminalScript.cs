using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    public GameObject guiObject;
    private bool isInTrigger = false;
    public int key = 1;
    // Start is called before the first frame update
    void Start()
    {
        guiObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInTrigger = true;
            guiObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInTrigger = false;
            guiObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerManager.GainKey(key);
                key = 0;
            }
        }
    }
}
