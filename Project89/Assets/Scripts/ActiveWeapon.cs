using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    //private GameObject weapon;

    public static string currentActiveWeapon = "Pistol"; // ¬ынести в глобальный статический класс
    // Start is called before the first frame update
    void Start()
    {
       // weapon = gameObject.transform.GetChild(0).gameObject;
        foreach (Transform weaponEq in transform)
        {
            if (weaponEq.name == currentActiveWeapon)
                weaponEq.gameObject.SetActive(true);
            else
                weaponEq.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Equip(string newWeaponName)
    {
        foreach (Transform weaponEq in transform)
        {
            if (weaponEq.name == newWeaponName)
            {
                weaponEq.gameObject.SetActive(true);
                currentActiveWeapon = newWeaponName;
            }
            else
                weaponEq.gameObject.SetActive(false);
        }
       /* Destroy(weapon);
        weapon = newWeapon;
        weapon.transform.parent = gameObject.transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);*/
    }
}
