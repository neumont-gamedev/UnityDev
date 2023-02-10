using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public virtual bool isEquipped { get; set; } = false;

	public abstract void Equip();
	public abstract void Unequip();

	public abstract void Use();
	public abstract void StopUse();

	public abstract bool isReady();

}
