using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;
    Weapon equippedWeapon;

    void Awake()
    {
        //Melakukan copy dari weaponHolder
        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)
        {
            //Mononaktifkan weapon
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Jika player menyentuh weapon pickup
        if (other.gameObject.CompareTag("Player"))
        {
            //Menghapus weapon yang ada di player
            Weapon temp = other.GetComponentInChildren<Weapon>();
            if (temp != null)
            {
                Destroy(temp.gameObject);
            }

            //Menginstansiasi weapon baru
            equippedWeapon = Instantiate(weapon);

            //Mengatur parent dan posisi weapon
            equippedWeapon.transform.SetParent(other.transform);
            equippedWeapon.transform.position = other.transform.position;

            //Mengatur visual weapon yang sudah diambil
            TurnVisual(true, equippedWeapon);

            //Menonaktifkan weapon pickup
            gameObject.SetActive(false);
        }
    }

    void TurnVisual(bool on)
    {
        weapon.gameObject.SetActive(on);
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        weapon.gameObject.SetActive(on);
    }
}