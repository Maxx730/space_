using UnityEngine;
using System.Collections;

public class EnemyGroup : MonoBehaviour {

	GameObject unit_manager;
	GameObject target_group;


	GameObject cur_tar;
	GameObject last_tar;

	// Use this for initialization
	void Start () {
		//GRAB OUR UNIT MANAGER SO THAT WE CAN 
		//FIND THE CLOSEST PLAYER UNIT GROUP.
		unit_manager = GameObject.Find("UnitManager");
		target_player_unitgroup();
	}
	
	// Update is called once per frame
	void Update () {
		check_target_distance();
	}

	//LOOP THROUGH EACH UNIT GROUP AND FIND THE CLOSEST ONE BY CHECKING THE DISTANCE FROM
	//THE FIRST UNIT IN THE UNIT GROUP.
	void target_player_unitgroup(){
		foreach(GameObject group in unit_manager.transform.GetComponent<UnitManager>().player_groups){
			Transform[] t = group.GetComponentsInChildren<Transform>();

			if(cur_tar != null){
				if(get_grp_distance(t[1].position) < get_grp_distance(cur_tar.transform.position)){
					last_tar = cur_tar;
					cur_tar = t[get_random_target()].gameObject;
				}
			}else{
				cur_tar = t[get_random_target()].gameObject;
			}
		}

		//SET THE ENEMY UNIT GROUP UNITS TARGET TO THE CLOSEST PLAYER UNIT GROUP.
		transform.GetComponent<UnitGroup>().set_targets(cur_tar.transform.position);
	}

	//TAKES IN A TARGET SUCH AS A UNITS TRANSFORM POSITION
	//AND RETURNS A FLOAT REPRESENTING THE DISTANCE BETWEEN THE TARGET AND 
	//
	float get_grp_distance(Vector3 grp_position){
		return Vector3.Distance(transform.position, grp_position);
	}

	void check_target_distance(){
		if(cur_tar != null){
			if(Vector3.Distance(transform.position,cur_tar.transform.position) > 5f && transform.GetComponent<UnitGroup>().all_arrived == true){
				transform.GetComponent<UnitGroup>().set_targets(cur_tar.transform.position);
			}
		}
	}

	int get_random_target(){

		if(cur_tar != null){
			return Random.Range(0,cur_tar.GetComponent<UnitGroup>().units.Count);
		}else{
			return 0;
		}
	}
}