using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip[] audioClips;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
			audioSource.Play();
		}
	}
}
