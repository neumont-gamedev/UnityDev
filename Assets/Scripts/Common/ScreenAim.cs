using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAim : MonoBehaviour
{
    [SerializeField] float distance;

    private Camera mainCamera;

    void Start()
    {
		mainCamera = Camera.main;
	}

    void Update()
    {
        // get a world position using the camera viewport (screen) center (0.5, 0.5) a distance (Z) away
        // set this game object at that world position
		transform.position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distance));
	}
}
