using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitGroup : MonoBehaviour {

	public List<Transform> units = new List<Transform>();

	//Each group of units will have a leading ship, this will be
	//used for finding points on the map etc.
	public GameObject group_leader;
	public bool wandering = false;
	public GameObject group_tag;
	public GameObject move_target_redicule;
	public Vector3 group_movement_target;
	public bool all_arrived;
	public bool is_rotating;

	// Use this for initialization
	void Start () {
		move_target_redicule = GameObject.Find("ClickTarget");
		reorder_group();	
	}
	
	// Update is called once per frame
	void Update () {
		//MAKE THE POSITION OF THE GROUP TAG THE SAME AS THE FIRST UNIT IN THE GROUP.
		//group_tag.transform.position = new Vector3(units[0].transform.position.x,units[0].transform.position.y,-57.7f);
		check_arrived();
	}

	//USE THIS FUNCTION TO REORDER UNITS AFTER THEY HAVE BEEN DESTROYED AS WELL.
	public void reorder_group(){
		units.Clear();
		//IF THERE ARE NO UNITS LEFT IN THE GROUP THEN DESTROY THE GROUP.
		foreach(Transform unit in transform){
			if(unit.tag.Contains("Unit")){
				units.Add(unit);
				unit.GetComponent<GenericUnit>().in_group = true;
			}
		}				
		Debug.Log("Units in Group: "+units.Count);
	}

	//LOOP THROUGH EACH UNIT IN THE GROUP AND TURN ON THEIR REDICULE, THIS WILL
	//MAKE THE CLICK EVENT SCRIPT EASIER TO READ/HANDLE.
	public void highlight_group(){
		foreach(Transform unit in units){
			unit.GetComponent<GenericUnit>().redicule.GetComponent<Renderer>().enabled = true;
		}
	}

	//DEHIGHLIGHTS THE REDICULE FOR ALL THE UNITS IN THE GROUP.
	public void unhighlight_group(){
		foreach(Transform unit in units){
			unit.GetComponent<GenericUnit>().redicule.GetComponent<Renderer>().enabled = false;
		}
	}

	//WHEN A UNIT IS DESTROYED WE WANT TO REMOVE THE UNIT FROM THE UNIT GROUP LIST.
	public void remove_destroyed(Transform unit){
		units.Remove(unit);
	}

	//SET THE MOVEMENT TARGET FOR EACH UNIT IN THE GROUP.
	public void set_targets(Vector3 movement_target){
		all_arrived = false;
		is_rotating = false;
		Vector3 world_point = Camera.main.ScreenToWorldPoint(movement_target);
		move_target_redicule.transform.position = new Vector3(world_point.x,world_point.y,-57.7f);
		move_target_redicule.GetComponent<Renderer>().enabled = true;
		group_movement_target = movement_target;

		foreach(Transform unit in units){
			unit.GetComponent<GenericUnit>().set_target(movement_target);
		}
	}

	//WE WANT TO SET A POINT FOR EACH INDIVIDUAL UNIT TO ROTATE
	//AROUND WHEN THEY ARE NOT MOVING SOMEWHERE OR ENGAGING AN ENEMY.
	public void set_rotation_target(){
		is_rotating = true;

		foreach(Transform unit in units){

		}
	}

	//CHECKS IF EACH UNIT IN THE UNIT GROUP HAS ARRIVED WITHIN A CERTAIN 
	//DISTANCE OF THE GIVEN MOVMENT TARGET.
	public void check_arrived(){
		int i = 0;

		//MAKE SURE THERE IS A MOVEMENT TARGET SET
		if(group_movement_target != null){
 			foreach(Transform unit in units){
 				if(unit.GetComponent<GenericUnit>().arrived_target){
 					i++;
 				}
 			}
		}

		//CHECK IF ALL OF THE UNITS HAVE ARRIVED.
		if(i == units.Count){
			all_arrived = true;
			set_rotation_target();
		}
	}
}
