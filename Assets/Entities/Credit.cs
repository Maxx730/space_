using UnityEngine;
using System.Collections;

//GAMEOBJECT THAT WILL REPRESENT MONEY, THIS IS MOSTLY FOR A PHYSICAL
//REPRESENTATION RATHER THEN THE LOGICAL ONE.
public class Credit : MonoBehaviour {

	//HOW MUCH THIS CREDIT IS WORTH.
	public int value;

	// Use this for initialization
	void Start () {
		Debug.Log("I am a credit.");
	}
}
