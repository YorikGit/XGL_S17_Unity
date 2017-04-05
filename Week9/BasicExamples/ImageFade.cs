using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour {


	public Image fadeImage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Color c = fadeImage.color;
		c.a -= 0.01f;
		fadeImage.color = c;
	}
}
