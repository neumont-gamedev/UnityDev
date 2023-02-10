using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
	PROJECTILE,
	INSTANT,
	VOLUME
}

[CreateAssetMenu(fileName = "Ammo", menuName = "Weapon/Ammo")]
public class AmmoData : ItemData
{
	public AmmoType ammoType;
	public float lifetime;
	public float force;
	public float damage;
	public ForceMode forceMode;
	public bool damageOverTime;
	public bool bounce;
	public bool rotateToVelocity;
	public bool impactOnExpired;
	public GameObject impactPrefab;
}
