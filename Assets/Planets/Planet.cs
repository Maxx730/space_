using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Planet : MonoBehaviour {

	//This is the planet info canvas that we are going
	//to change to have the planet autofind this object as
	//opposed to setting it up manually.
	private Canvas can;

	//Planet Info text fields.
	Text pName;
	Text pSize;
	Text pLocation;
	Text pFaction;
	Text pYearLength;

	public Transform redicule;

	private bool planet_pause;

	private GameObject info;

	//Different planet will have different sizes, etc.
	public int planet_size;
	public string planet_name;
	public string faction;
	//In days.
	public int year_length;

	//The target that the planet is going to orbit around.
	public GameObject target_orbit;
	public float orbit_speed;

	//Location in actual map space.
	public int location_x;
	public int location_y;

	// Use this for initialization
	void Start () {
		planet_pause = false;

		//Find our redicule object and hide it initially.
		redicule = transform.GetChild(0);
		redicule.GetComponent<Renderer>().enabled = false;

		//Find the planet info canvas.
		can = GameObject.Find ("PlanetInfo").GetComponent<Canvas>();
		info = GameObject.Find ("PlanetInfo");
	}
	
	// Update is called once per frame
	void Update () {
		if(target_orbit != null){
			transform.RotateAround (target_orbit.transform.position, new Vector3(1,1,-15), orbit_speed * Time.deltaTime);
		}
	}

	//When a user clicks on a planet certain actions need to be
	//taken.
	void OnMouseDown(){
		//Pause on click code.
		/*if(planet_pause){
			Time.timeScale = 1;
			planet_pause = false;
		}else{
			Time.timeScale = 0;
			planet_pause = true;
		}*/

		//Make sure to reset the focused planet redicule.
		if(info.GetComponent<PlanetInfoScript> ().focused_planet != null){
			info.GetComponent<PlanetInfoScript> ().focused_planet.GetChild(0).GetComponent<Renderer> ().enabled = false;
		}

		can.enabled = true;
		can.GetComponent<PlanetInfoScript>().set_current_planet (transform);
		redicule.GetComponent<Renderer> ().enabled = true;
		set_planet_info ();		
	}

	//Loop through the child objects and use a switch statement
	//to set info based on childobject name.
	void set_planet_info(){
	
		//Find all the text fields to fill with information.
		pName = can.transform.Find ("PlanetName").GetComponent<Text>();
		pSize = can.transform.Find ("PlanetSize").GetComponent<Text>();
		pLocation = can.transform.Find ("PlanetLocation").GetComponent<Text>();
		pFaction = can.transform.Find ("Faction").GetComponent<Text>();
		pYearLength = can.transform.Find ("Year").GetComponent<Text>();

		//Set the information fields in the canvas.
		pName.text = "Name :" + planet_name;
		pSize.text = "Size :" + planet_size + "km";
	}
}
