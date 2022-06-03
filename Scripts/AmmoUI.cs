using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{

	public Transform weapons;

	WeaponSwitching cSwitching;
	PlayerShooting cShooting;
	int currentWeapon;
	Text text;



	void Start ()
	{
		text = GetComponent <Text> ();
		currentWeapon = 0;
		cSwitching = weapons.gameObject.GetComponent<WeaponSwitching> ();
		ChangeWeapon ();
	}


	void Update ()
	{
		
		if (cSwitching.selectedWeapon != currentWeapon) {
			currentWeapon = cSwitching.selectedWeapon;
			ChangeWeapon ();
		}
		text.text = cShooting.currentAmmoInMagazine + " / " + cShooting.currentAmmo;
	}

	void ChangeWeapon ()
	{
		int i = 0;
		foreach (Transform weapon in weapons) {
			if (i == cSwitching.selectedWeapon) {
				cShooting = weapon.gameObject.GetComponent<PlayerShooting> ();		
			}
			i++;
		}


	}
}