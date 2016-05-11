using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemScript : MonoBehaviour {

	//The star that everything essentially revolves around.
	public GameObject system_star;
	public string system_name;

	private List<GameObject> systems = new List<GameObject>();

	// Use this for initialization
	void Start () {
		findSystems();
		Debug.Log(systems.Count);
		drawPaths();
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Function that draws paths between all of the star systems on our map.
	void findSystems(){
		//Grab all the objects in the scene.
		GameObject[] getSystems = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
	
		//Loop through each object and check to see if its a star system, if so keep track of the 
		//star system and all of its planets.
		foreach(GameObject system in getSystems){
			if(system.tag == "StarSystem"){
				systems.Add(system);
			}
		}	
	}

	//Draw paths between all the star systems in the area.
	void drawPaths(){
		GameObject prev = new GameObject();
		foreach(GameObject system in systems){
			//Check if the system is equal to itself.
			if(system.GetInstanceID() != prev.GetInstanceID()){
				Debug.Log("its already me!");
				prev = system;
			}
		}
	}
}
