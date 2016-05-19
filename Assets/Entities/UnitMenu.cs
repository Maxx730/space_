using UnityEngine;
using System.Collections;

public class UnitMenu : MonoBehaviour {

	//CHANGES DEPENDING ON IF WE ARE RIGHT CLICKING ON
	//AN ENEMY UNIT OR A PLAYER UNIT.
	public string context;
	public GameObject player_control;
	public GameObject panel;

	void Start(){
		hide_canvas();
	}
	
	// Update is called once per frame
	void Update () {
		if(player_control != null && player_control.GetComponent<PlayerControl>().focused_unit){
			transform.position = new Vector3(player_control.GetComponent<PlayerControl>().focused_unit.GetComponent<UnitGroup>().units[0].position.x + 5,player_control.GetComponent<PlayerControl>().focused_unit.GetComponent<UnitGroup>().units[0].position.y + 5,-57.7f);
		}
	}

	//FUNCTIONS BELOW ARE FOR HIDING AND SHOWING THE CANVAS.
	public void hide_canvas(){
		panel.SetActive(false);
	}

	public void show_canvas(){
		panel.SetActive(true);
	}
}
