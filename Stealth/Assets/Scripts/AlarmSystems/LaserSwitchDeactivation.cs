using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour {

	public GameObject laser;
	public Material unlockedMat;

	private GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player) {
		
			if (Input.GetButton ("Switch")) {
				LaserDeactivation();
			}
		}

	}

	void LaserDeactivation()
	{
		laser.SetActive (false);

		Renderer screen = transform.Find ("prop_switchUnit_screen_001").GetComponent<Renderer> ();
		screen.material = unlockedMat;

		var audio = this.gameObject.GetComponent<AudioSource> ();
		audio.Play ();
	}
}
