using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public Collider[] attackHitboxes;
    public float damage = 15f;
    private GameObject boxObject;
    public Renderer thisRenderer;
    public float timeVisible = 0.3f;
    private bool isPressed = false;
    // Start is called before the first frame update
    private void Awake()
    {
        thisRenderer = gameObject.GetComponent<Renderer>();
        thisRenderer.enabled = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPressed = true;
            LaunchAttack(attackHitboxes[0]);
        }
        
        if (isPressed && timeVisible > 0)
        {
           // thisRenderer.enabled = true;
            timeVisible -= Time.deltaTime;
        } 
        else
        {
            isPressed = false;
            timeVisible = 0.3f;
            //thisRenderer.enabled = false;
        }
    }

    private void LaunchAttack(Collider col)
    {
        //thisRenderer.enabled = true;
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Target"));
        foreach (Collider c in cols)
        {
            c.SendMessageUpwards("TakeDamage", damage);
        }
        Debug.Log("LOL");
        //thisRenderer.enabled = false;
    }
}
