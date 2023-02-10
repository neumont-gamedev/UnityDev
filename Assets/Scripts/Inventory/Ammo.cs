using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class Ammo : Interactable
{
	[SerializeField] private AmmoData ammoData;

	private void Start()
	{
		if (ammoData.ammoType == AmmoType.PROJECTILE)
		{
			GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ammoData.force, ammoData.forceMode);
		}

		GetComponent<CollisionEvent>().onEnter += OnInteract;
		Destroy(gameObject, ammoData.lifetime);
	}

	public override void OnInteract(GameObject target)
	{
		Debug.Log(target.name);
		if (target.TryGetComponent<Health>(out Health health))
		{
			health.OnApplyDamage(ammoData.damage * ((ammoData.damageOverTime) ? Time.deltaTime : 1));
		}
		if (ammoData.impactPrefab != null) 
		{
			Instantiate(ammoData.impactPrefab, target.transform.position, target.transform.rotation);
		}
	}

}
