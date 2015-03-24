﻿using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {

	public bool requireKey;
	public AudioClip doorSwishClip;
	public AudioClip accessDeniedClip;

	private Animator anim;
	private HashIDs hash;
	private GameObject player;
	private PlayerInventory playerInventory;
	private int count;

	void Awake()
	{
		anim = this.gameObject.GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory = player.GetComponent<PlayerInventory> ();
	}

	void OnTriggerEnter(Collider other)
	{	
		var audio = this.gameObject.GetComponent<AudioSource> ();
		if (other.gameObject == player) {
		
			if (requireKey) {
				if (playerInventory.hasKey) {
					count++;
				} else {
					audio.clip = accessDeniedClip;
					audio.Play ();
				}
			} else {
				count++;
			}
		} else if (other.gameObject.tag == Tags.enemy) {
			if(other is CapsuleCollider)
			{
				count++;
			}

		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player || (other.gameObject.tag == Tags.enemy && other is CapsuleCollider)) {
			count = Mathf.Max(0,count-1);
		}
	}

	void Update()
	{
		var audio = this.gameObject.GetComponent<AudioSource> ();

		anim.SetBool (hash.openBool, count > 0);

		if (anim.IsInTransition (0) && !audio.isPlaying) {
			audio.clip = doorSwishClip;
			audio.Play();
		}
	}
}
