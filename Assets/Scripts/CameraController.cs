using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform followTargetTransform;
	[SerializeField] private float lookTurnRate = 0.1f;
	[SerializeField] private float yawLimit = 60;
	[SerializeField] private float pitchLimit = 40;

	[SerializeField] private InputRouter inputRouter;

	Vector2 inputAxis;

	public Transform followTransform { get; set; }

	void Start()
	{
		inputRouter.lookEvent += OnLook;
	}

	void Update()
	{
		// rotate follow target transform
		Quaternion qyaw = Quaternion.AngleAxis(inputAxis.x * lookTurnRate, Vector3.up);
		Quaternion qpitch = Quaternion.AngleAxis(-inputAxis.y * lookTurnRate, Vector3.right);

		followTargetTransform.rotation *= (qyaw * qpitch);

		// clamp rotation (get euler angles, ignore roll rotation (z))
		var rotation = followTargetTransform.localEulerAngles;
		rotation.z = 0;

		// clamp pitch (pitch is rotation around the x angle)
		float pitch = rotation.x;
		if (pitchLimit != 0)
		{
			if (pitch > 180 && pitch < (360 - pitchLimit)) pitch = (360 - pitchLimit);
			else if (pitch < 180 && pitch > pitchLimit) pitch = pitchLimit;
		}

		// clamp yaw (yaw is rotation around the y angle)
		float yaw = rotation.y;
		if (yawLimit != 0)
		{ 
			if (yaw > 180 && yaw < (360 - yawLimit)) yaw = 360 - yawLimit;
			else if (yaw < 180 && yaw > yawLimit) yaw = yawLimit;
		}

		rotation.x = pitch;
		rotation.y = yaw;

		followTargetTransform.transform.localEulerAngles = rotation;
	}

	public void OnLook(Vector2 axis)
	{
		inputAxis = axis;
	}
}
