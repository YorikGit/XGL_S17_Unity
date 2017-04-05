using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityOscillate : MonoBehaviour {

	// Use this for initialization

	Light l;
	void Start () {
		l = GetComponent <Light> ();
	}

	float timer = 0f;
	float period = 2f;

	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		// This code ensures timer is always between [0,period]
		if (timer >= period)
			timer -= period;
		

		// Gives us -1 to 1
		float sinValue = Mathf.Sin (6.28f * (timer/period));

		// Intensity will oscillate between 0 and 0.6.
		l.intensity = 0.3f + 0.3f * sinValue;
	}
}
