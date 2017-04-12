using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Needed for FileStream and File
using System.IO;

// Needed for BinaryFormatter
using System.Runtime.Serialization.Formatters.Binary;

// Needed for [Serializable]
using System;


public class SaveManager : MonoBehaviour  {

	// This just tests the saving and loading
	// These functions are static because you want to be able to call functions in the
	// SaveManager script without attaching it to a gameobject.
	static public void Test() {

		gold = 10;
		beatGame = true;
		playerName = "Sean 谷";
		eventState = new List<bool> { false, true, false };
		position = new Vector3 (10, 30, 60);

		//_Save ();

		_Load ();
	}

	static void _Save() {

		// File.Create() makes an empty save file.
		// A FileStream manages an open file (which is able to be written to)
		// Application.per... is a location on the player's computer where it's safe to save data.
		FileStream file = File.Create (Application.persistentDataPath + "/save.txt");

		// Create an instance of SaveData, fill out its properties.
		SaveData data = new SaveData ();
		data.beatGame = beatGame;
		data.gold = gold;
		data.eventState = eventState;
		data.playerName = playerName;
		data.position = new SaveVector3(position);


		// BinaryFormatter is needed to save the data as a binary (0s and 1s) file.
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize (file,data);

		// Don't forget to Close() the file, or bad things could happen!
		file.Close ();

	}

	static void _Load() {

		// Basically the same as _SavE() but backwards.
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/save.txt", FileMode.Open);
		SaveData data = (SaveData)bf.Deserialize (file);
		file.Close ();

		beatGame = data.beatGame;
		gold = data.gold;
		playerName = data.playerName;
		eventState = data.eventState;
		position = data.position.toVector3 ();

		// Verify the data was written
		print (beatGame);
		print (position); 
		print (gold);
		print (playerName);
		foreach (bool b in eventState) {
			print (b); 
		}
	}

	// Q: Why do we have the same variables here as in the SaveData class below?
	// A: Because these variables are what we actually modify with other game code - they're
	// 	what other code should reference when figuring things out. The varaibles in the SaveData
	//	class are just for when you need to save all this to a file.
	static public Vector3 position;
	static public int gold;
	static public bool beatGame;
	static public string playerName;
	static public List<bool> eventState;

}


// You need to make a "SaveData" class - which the Save() function will create an instance of,
// in order to save data.

// This is a special keyword that tells Unity you want to be able to save and load this class
// You would list all of the data you need saved inside of here. For most simple games,
// this is probably something like the player's position, the current scene, and 
// some event flags.
[Serializable]
class SaveData {
	public int gold = 0;
	public bool beatGame = false;
	public string playerName = "";
	public SaveVector3 position;
	public List<bool> eventState = null;
}

// Because Vector3 is specific to Unity it can't be saved automatically... therefore
// we need to make this extra class.

// It's pretty simple - the Constructor (the function called when you want to make a new instance
// of SaveVector3 - or , 'public SaveVector3(Vector3 v)' - is used to set the 
// properties of the new SaveVector3.
// Then when loading data, my SaveData's SaveVector3 will have toVector3() called on it
// in order to set the Vector3 'position'  of my Savedata that Unity can use.
[Serializable]
class SaveVector3 {
	public SaveVector3(Vector3 v) {
		x = v.x;
		y = v.y;
		z = v.z;
	}

	public Vector3 toVector3() {
		return new Vector3(x,y,z);
	}
	float x;
	float y;
	float z;
}
