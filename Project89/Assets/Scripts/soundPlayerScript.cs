using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPlayerScript : MonoBehaviour
{
    private AudioSource temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = gameObject.GetComponent<AudioSource>();
        temp.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!temp.isPlaying)
            Destroy(gameObject);
    }
}
