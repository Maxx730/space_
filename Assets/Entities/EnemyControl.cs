using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public bool wandering = false;

	// Use this for initialization
	void Start () {
		find_target();
	}
	
	// Update is called once per frame
	void Update () {

	}

	//If there are player units on the map,find the closest unit
	//and target it.
	void find_target(){
		//Loop through each unit group and find the closest one that is
		//a player unit group.
		foreach(GameObject group in GameObject.FindGameObjectsWithTag("Grp")){
			//Keep track of the unit groups distance.
			float last_distance = 0;
			GameObject current_tar;

			//Enemies will only go after player units (for now)
			if(group.name == "OrangeUnitGroup"){
				float check_distance = Vector3.Distance(transform.position,group.transform.position);
				
				if(last_distance != 0){
					if(check_distance < last_distance){
						last_distance = check_distance;
						current_tar = group;
					}
				}else{
					//Set the last closest distance and keep track
					//of which object it was.
					last_distance = check_distance;
					current_tar = group;
				}
			}
		}

		
	}
}
