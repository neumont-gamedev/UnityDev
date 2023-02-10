using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	[SerializeField] Item[] items;

	public Item currentItem;

	private void Start()
	{
		currentItem = items[0];
		currentItem.Equip();
	}



}
