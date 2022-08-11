using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysCounter : MonoBehaviour
{
    public Text keysText;
    private int keyCount;
    // Start is called before the first frame update
    void Start()
    {
        keyCount = PlayerManager.keyToOpenFront;
    }

    // Update is called once per frame
    void Update()
    {
        keyCount = PlayerManager.keyToOpenFront;
        keysText.text = "KEYS>> " + keyCount;
    }
}
