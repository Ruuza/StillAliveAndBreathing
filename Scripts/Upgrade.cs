using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {

    public GameObject purchaseText;

    public int AffectedWeapon;

    
    public bool addAmmo;
    public bool upgradeWeapon;
    public bool heal;

    public float affectAmount;
    public int price;

    public bool removeAfterUse;

    public GameObject player;

    public GameObject[] weapons;

    private bool playerInRange;


	void Start () {
        playerInRange = false;
        
	}
	
	void Update () {

        if (playerInRange)
        {
            if (MoneyManager.money >= price) {

                if (Input.GetKeyDown(KeyCode.F))
                {
                    MoneyManager.money -= price;

                    

                    if (addAmmo)
                    {
                        PlayerShooting playerShooting = weapons[AffectedWeapon].GetComponent<PlayerShooting>();

                        
                       
                            playerShooting.currentAmmo += (int)affectAmount;

                    }
                    if (heal)
                    {
                        player.GetComponent<PlayerHealth>().HealPlayer();
                    }

                    if (upgradeWeapon)
                    {
                        PlayerShooting playerShooting = weapons[AffectedWeapon].GetComponent<PlayerShooting>();
                        playerShooting.maxAmmoInMagazine += 10;
                        playerShooting.damagePerShot += 5;
                        playerShooting.reloadTime -= 0.2f;

                        purchaseText.SetActive(false);
                        this.gameObject.SetActive(false);
                    }
                }

            }


        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) {
            playerInRange = true;
            purchaseText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            purchaseText.SetActive(false);
        }
    }
}
