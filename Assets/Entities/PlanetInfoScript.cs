using UnityEngine;
using System.Collections;

public class PlanetInfoScript : MonoBehaviour {

	//When user clicks on planet, we will set the current 
	//planet to the one that was clicked.
	public Transform focused_planet;
	Canvas can;

	// Use this for initialization
	void Start () {
		//Find the planet info canvas.
		can = GameObject.Find ("PlanetInfo").GetComponent<Canvas>();
		can.enabled = false;
	}

	//Take in the planets transform set it so the UIController
	//can access it to turn off the redicule. (Or other things.)
	public void set_current_planet(Transform t){
		focused_planet = t;	
	}

	public void close_planet_info(){
		focused_planet.GetChild(0).GetComponent<Renderer> ().enabled = false;
		can.enabled = false;
	}

}
