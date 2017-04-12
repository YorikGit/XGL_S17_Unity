using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Attach this script to a game object to test out the save system.
// In an actual game, you'd probably make an object with a script that uses
// the SaveManager functions.

public class SaveTester : MonoBehaviour  {

	void Start (){
		SaveManager.Test ();
	}


}


