using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour {
	//OBJECT THAT WILL KEEP TRACK OF EVERY UNIT ON THE MAP, REGARDLESS
	//OF WHAT SIDE THE ENEMY IS ON.  IT WILL ALSO KEEP TRACK OF ALL THE UNIT GROUPS
	//AS WELL AS WHICH UNITS ARE IN EACH UNIT GROUP.
	public int number_of_units;
	public int number_of_groups;

	//LIST THAT HOLDS EVERY SINGLE UNIT ON THE MAP.
	public ArrayList all_units = new ArrayList();
	public ArrayList all_groups = new ArrayList();

	//LISTS THAT HOLD DIFFERENT TYPES OF UNITS.
	public ArrayList player_units = new ArrayList();
	public ArrayList enemy_units = new ArrayList();

	//KEEP TRACK OF ALL THE DIFFERENT UNIT GROUPS ON THE MAP.
	public ArrayList enemy_groups = new ArrayList();
	public ArrayList player_groups = new ArrayList();

	// Use this for initialization
	void Start () {
		update_info();
		grab_unit_groups();
	}
	
	// Update is called once per frame
	void Update () {
		display_info();
	}

	//WE USE THIS FUNCTION TO COUNT DIFFERENT OBJECTS AND UPDATE THE GLOBAL 
	//INFO.
	void update_info(){
		//Grab every element on the map and find all units.
		GameObject[] all_objects = UnityEngine.Object.FindObjectsOfType<GameObject>();
		
		foreach(GameObject obj in all_objects){
			if(obj.tag.Contains("Unit")){
				if(obj.tag.Contains("Pu")){
					player_units.Add(obj);
					all_units.Add(obj);
				}else if(obj.tag.Contains("Enemy")){
					enemy_units.Add(obj);
					all_units.Add(obj);
				}
			}else if(obj.tag.Contains("Grp")){
				all_groups.Add(obj);
			}
		}
	}

	//LOOPS THROUGH ALL THE UNIT GROUPS AND CHECKS THE FIRST 
	//UNIT TO DETERMINE IF THE UNIT GROUP IS AN ENEMY OR IF
	//THE UNIT GROUP IS A PLAYER UNIT GROUP.
	void grab_unit_groups(){
		foreach(GameObject grp in all_groups){
			Transform[] t = grp.GetComponentsInChildren<Transform>();

			if(t[1].gameObject.tag.Contains("Pu")){
				player_groups.Add(grp);
			}else{
				enemy_groups.Add(grp);
			}
		}
	}

	//DISPLAY PERTINENT INFORMATION FOR DEBUGGING PURPOSES.
	void display_info(){
		string info = "Player Units:"+player_units.Count+" EnemyUnits:"+enemy_units.Count+" Units:"+all_units.Count+" Player Groups:"+player_groups.Count+" Enemy Groups:"+enemy_groups.Count;
		Debug.Log(info);
	}

	//WHEN WE WANT TO RECOUNT ALL THE UNITS ON THE FIELD WE NEED TO EMPTY OUT
	//THE INFORMATION IN EACH ARRAY. THEN RECALCULATE EVERYTHING TO DETERMINE
	//UPDATES ON UNITS ON THE MAP.
	public void reset_information(){
		player_units.Clear();
		enemy_units.Clear();
		all_units.Clear();
		update_info();
	}
}
