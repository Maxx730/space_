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
	}

	//LOOP THROUGH EACH UNIT GROUP AND FIND THE CLOSEST ONE BY CHECKING THE DISTANCE FROM
	//THE FIRST UNIT IN THE UNIT GROUP.
	void target_player_unitgroup(){
		foreach(GameObject group in unit_manager.transform.GetComponent<UnitManager>().player_groups){
			Transform[] t = group.GetComponentsInChildren<Transform>();

			if(cur_tar != null){
				if(get_grp_distance(t[1].position) < get_grp_distance(cur_tar.transform.position)){
					last_tar = cur_tar;
					cur_tar = t[1].gameObject;
				}
			}else{
				cur_tar = t[1].gameObject;
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
}