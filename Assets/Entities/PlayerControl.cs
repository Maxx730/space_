using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Object that the player will use to control ships under 
//their faction, this will be an 'empty' game object that will
//really only deal with telling different units what they should be 
//doing.
public class PlayerControl : MonoBehaviour {

	public string player_name;
	public int player_score;
	public string faction_name;

	//List of units under the players current control.

	//When a player clicks on a single unit, it will be focused upon
	//that single unit for specific tasks,targets etc.
	public GameObject targeted_unit;
	public Transform focused_unit;

	//When the player clicks to move a unit this will indicate where the 
	//player has clicked on the map.
	public GameObject target_redicule;
	private GameObject enemy_targeted;
	public GameObject enemy_target_redicule;

	private float buttonDownTime = 0.0f;
	private bool clickHold = false;
	private float buttonUpTime;

	public Text gameMode;
	private bool reClick;
	private float tarStartScale;
	private float endTarScale;

	//Variables used for selecting multiple units.
	private Vector3 begin_selection;
	private Vector3 end_selection;
	private bool isDragging;
	private bool hasBegun;

	private bool squadSelected;

	// Use this for initialization
	void Start () {

		reClick = false;

		tarStartScale = .3f;
		endTarScale = .6f;

		//Set our focused unit to null because there is not one.
		focused_unit = null;

		squadSelected = false;

		//Find the target redicule for displaying clicked targets.
		target_redicule = GameObject.Find ("ClickTarget");
		target_redicule.GetComponent<Renderer> ().enabled = false;

		//Get our targeted enemy sprite and hide it.
		enemy_targeted = GameObject.Find("TargetedEnemy");
		enemy_targeted.GetComponent<Renderer>().enabled = false;
	}

	//Draw our selection box if the user is dragging.
	void OnGUI () {
		if(isDragging){
			if(hasBegun){
				begin_selection = Event.current.mousePosition;
				hasBegun = false;
			}

			Texture2D texture = new Texture2D(1, 1);
	    	texture.SetPixel(0,0,new Color32(255,255,255,75));
	    	texture.Apply();
	    	GUI.skin.box.normal.background = texture;
	    	GUI.Box( new Rect(begin_selection.x,begin_selection.y,Event.current.mousePosition.x-begin_selection.x,Event.current.mousePosition.y-begin_selection.y), GUIContent.none);
		}else{
			
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(targeted_unit != null){
			enemy_target_redicule.transform.position = targeted_unit.transform.position;
		}

		//If there is a selected unit and the player right clicks then it will unfocus from
		//any unit on the map.
		if(Input.GetMouseButtonDown(1)){
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			//Check that we have hit something and that there is a focused unit.
			//Check if the raycast has actually hit another gameobject.
			if(hit){
				if(hit.collider.gameObject.tag.Contains("EnemyUnit")){

				}
			}else{
				//If the raycast has not hit anything, then we will  either
				//move the focused unit or we will do nothing.
				if(focused_unit != null){
					//Check if we have focused on a group or not.
					if(focused_unit.gameObject.tag.Contains("Grp")){
						focused_unit.GetComponent<UnitGroup>().GetComponent<FormationHandler>().clear_formation_targets();
						focused_unit.GetComponent<UnitGroup>().set_targets(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-57.7f));
					}
				}
			}
		}

		//Determines what we want to do when the user
		//clicks on the map such as moving to a certain spot.
		//Works only with a left click.
		if(Input.GetMouseButtonDown(0)){
		    //Create a 2D raycast to check if a sprite has been clicked upon.
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		
			if(hit){
				Debug.Log(hit.collider.gameObject.tag);
				//Figure out what we are hitting before taking specific actions.
				if(hit.collider.gameObject.tag.Contains("Pu")){
					//Check if the unit is part of a group, if so
					//then we want to set the unit group to the focused unit.
					if(hit.collider.gameObject.GetComponent<GenericUnit>().in_group){
						GameObject group = hit.collider.gameObject.transform.parent.gameObject;
						change_focused(group);
					}
				}else if(hit.collider.gameObject.tag == "EnemyUnit"){
					if(focused_unit != null){

					}
				}
			}else{
				//If we do not hit anything with the raycast then unfocus
				//everything on the map.
				reset_redicules();
			}
		}
	}


	//TARGETING FUNCTIONS FOR THE REDICULE SHOWING OR HIDING
	//ONE WILL REMOVE ALL THE INDICATORS DEPENDING ON WHERE
	//THE USER CLICKS AND IF ITS A BACKGROUND OR NOT.
	void reset_redicules(){
		if(focused_unit != null){
			if(focused_unit.tag.Contains("Grp")){
				//Loop through units in group to turn off the redicule if 
				//the focused unit is a group.
				focused_unit.GetComponent<UnitGroup>().unhighlight_group();
				focused_unit = null;
			}
		}

		//Reset what we are focused on.
		focused_unit = null;
	}

	//Changes the focused unit if a unit what clicked on determined by the raycast hit.
	void change_focused(GameObject new_focused){
		//We need to check if we are clicking on a group of units or just a
		//single unit, if it is a group as well as a group of player 
		//units then highlight all the units.
		reset_redicules();

		if(new_focused.tag.Contains("Grp")){
			new_focused.GetComponent<UnitGroup>().highlight_group();

			//Set the group to the focused unit.
			focused_unit = new_focused.transform;
		}else{
			focused_unit = new_focused.transform;
			focused_unit.GetComponent<GenericUnit>().redicule.GetComponent<Renderer>().enabled = true;
		}
	}
}
