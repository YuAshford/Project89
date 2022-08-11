using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_System : MonoBehaviour
{
    public string weaponName;

    private GameObject gunToDrop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  /*  void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E))
            PickUp();
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("In Trigger");
            ActiveWeapon activeWeapon = other.gameObject.transform.GetChild(1).GetChild(0).GetComponent<ActiveWeapon>();
            // gunToDrop = other.gameObject.transform.GetChild(1).GetChild(0).GetComponent<GunSystem>();
            //Debug.Log(gunToDrop.name);
            if (activeWeapon)
            {
                //Drope();
                //var newWeapon = Instantiate(weapon);
                activeWeapon.Equip(weaponName);
            }
        }
    }

/*    private void Drope(GunSystem)
    {
        Instantiate();
    }*/
}
