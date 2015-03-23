using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

	public float fadeSpeed = 2f;
	public float highIntensity = 2f;
	public float lowIntenssity = 0.5f;
	public float changeMargin = 0.2f;
	public bool alarmOn;


	private Light light;
	private float targetIntensity;

	void Awake()
	{
		var lights = this.gameObject.GetComponents<Light> ();
		light = lights [0];
		light.intensity = 0f;
		targetIntensity = highIntensity;
	}

	void Update()
	{
		if (alarmOn) {
			light.intensity = Mathf.Lerp (light.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
		} else {
			light.intensity = Mathf.Lerp (light.intensity, 0f, fadeSpeed * Time.deltaTime);
		}
	}

	void CheckTargetIntensity()
	{
		if (Mathf.Abs (targetIntensity - light.intensity) < changeMargin) {
			if(targetIntensity == highIntensity)
			{
				targetIntensity = lowIntenssity;
			}else{
				targetIntensity = highIntensity;
			}
		}
	}
}
