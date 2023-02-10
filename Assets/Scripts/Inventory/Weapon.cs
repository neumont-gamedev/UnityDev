using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
	[SerializeField] WeaponData weaponData;
	[SerializeField] Animator animator;
	[SerializeField] Transform ammoTransform;

	private int ammoCount = 0;
	private bool weaponReady = false;


	public override void Equip()
	{
		isEquipped = true;
		weaponReady = true;
	}

	public override void Unequip()
	{
		isEquipped = false;
	}

	public override void Use()
	{
		if (weaponData.animTriggerName != "" && animator != null) 
		{ 
			Debug.Log("fire start");
			animator.SetTrigger(weaponData.animTriggerName);
		}
		else
		{
			Instantiate(weaponData.ammoPrefab, ammoTransform.position, ammoTransform.rotation);
		}
				
		if (weaponData.fireRate > 0)
		{
			weaponReady = false;
			StartCoroutine(ResetFireTimer());
		}
	}

	public override void StopUse()
	{
		Debug.Log("fire stop");
	}

	IEnumerator ResetFireTimer()
	{
		yield return new WaitForSeconds(weaponData.fireRate);
		weaponReady = true;
	}

	public override bool isReady()
	{
		return weaponReady && (ammoCount > 0 || weaponData.rounds == 0);
	}

	public void OnAnimEventFire()
	{
		Instantiate(weaponData.ammoPrefab, ammoTransform.position, ammoTransform.rotation);
	}
}
