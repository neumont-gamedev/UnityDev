using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	EQUIPMENT,
	WEAPON,
	AMMO
}

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
	public string id;
	public string description;
	public ItemType itemType;
	public Sprite icon;
	public GameObject itemPrefab;

}
