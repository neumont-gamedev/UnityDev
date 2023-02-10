using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode
{
	SINGLE,
	AUTOMATIC
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon")]
public class WeaponData : ItemData
{
	public FireMode fireMode;
	public float fireRate;
	public int rounds;
	public string animTriggerName;
	public GameObject ammoPrefab;
}
