using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public bool wandering = false;
	public ArrayList playerGroups = new ArrayList();

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
			if(group.name.Contains("OrangeUnitGroup")){
				playerGroups.Add(group);
			}
		}


	}

	//Once each enemy unit has found a target unit group, we want the 
	//the enemy unit to loop through and find a player unit within the
	//targeted group.
	void choose_random_unit(){

	}
}
