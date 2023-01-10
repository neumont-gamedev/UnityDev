using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Range(1, 10), Tooltip("controls the speed of the player")] public float speed = 5;
	public GameObject prefab;

	private void Awake()
	{
		Debug.Log("awake");
	}

	void Start()
	{
		Debug.Log("start");
	}

	void Update()
	{
		Vector3 direction = Vector3.zero;

		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");
		//if (Input.GetKey(KeyCode.A)) direction.x = -1;
		//if (Input.GetKey(KeyCode.D)) direction.x = +1;
		//if (Input.GetKey(KeyCode.W)) direction.z = +1;
		//if (Input.GetKey(KeyCode.S)) direction.z = -1;


		transform.position += direction * speed * Time.deltaTime;

		if (Input.GetButton("Fire1"))
		{
			Debug.Log("pew!");
			GetComponent<AudioSource>().Play();
			GameObject go = Instantiate(prefab, transform.position, transform.rotation);
			Destroy(go, 5);
		}
	}
}
