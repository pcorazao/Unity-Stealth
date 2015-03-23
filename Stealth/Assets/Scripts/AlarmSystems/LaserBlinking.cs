using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {

	public float onTime;
	public float offTime;

	private float timer;

	void Update()
	{
		timer += Time.deltaTime;

		var renderer = this.gameObject.GetComponent<Renderer> ();

		if (renderer.enabled && timer >= onTime) {
			SwitchBeam();
		}
		if (!renderer.enabled && timer >= offTime) {
			SwitchBeam();
		}

	}

	void SwitchBeam()
	{
		timer = 0f;

		var renderer = this.gameObject.GetComponent<Renderer> ();
		var light = this.gameObject.GetComponent<Light> ();

		renderer.enabled = !renderer.enabled;
		light.enabled = !light.enabled;
	}
}
