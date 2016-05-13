using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Generic Unit object will handle most of the most basic functions that 
//any unit on the map will have, regardless if its an enemy unit or 
//a player unit.  This is the most basic information about a unit in
//the game.
public class GenericUnit : MonoBehaviour {

	//Three different values to keep track of the ship,
	//one being the fastest it can travel and the other being
	//the current speed it is traveling at.

	//Check if the unit is part of a group 
	//and set accordingly from the unit group script.
	public bool in_group;

	public int max_health;
	private int current_health;
	public int max_speed;
	private int min_speed;
	public float cur_speed;

	public float rotation_speed;
	public int weapon_slots;
	public Transform redicule;
	private GameObject player_control;

	private bool selected;
	private bool isMoving;

	public Vector3 movement_target;
	private Vector3 direction_to;
	private bool cur_moving;
	private float target_angle;

	//Turning variables.
	private float incr_ang;
	private float total_distance;
	public GameObject target;
	public bool arrived_target;

	//Attempting to add thrust to have the ships move naturally.
	public Rigidbody2D rb;
	public bool engaging;
	public GameObject targeted_unit;
	public int unit_credit_worth;
	public int unit_scrap_worth;

	// Use this for initialization
	void Start () {

		//Find the player object whenever a unit is created, for commands
		//or even for targeting.
		player_control = GameObject.Find ("Player");

		//Find our redicule object and hide it initially.
		redicule = transform.GetChild(0);
		redicule.GetComponent<Renderer>().enabled = false;

		//Make sure the unit starts out not moving.
		cur_moving = false;

		//unit_info.enabled = false;
		//update_unit_info();
		engines_off();
	}
	
	// Update is called once per frame
	void Update () {
		//IF THERE IS A TARGET, ROTATE TOWARDS REGARDLESS
		//IF THE UNIT AS ALREADY ARRIVED.
		if(movement_target != null){
			rotate_unit();
		}
	}
	
	//Move sure the enemy is targeted at the player, we will have to 
	//program random offsets to help the player out at least in the beginning.
	void rotate_unit(){
		Quaternion newRotation;

		if(engaging){
			newRotation = Quaternion.LookRotation(transform.position - targeted_unit.transform.position, Vector3.forward);
		}else{
			newRotation = Quaternion.LookRotation(transform.position - movement_target, Vector3.forward);
		}

	    newRotation.x = 0.0f;
	    newRotation.y = 0.0f;
   		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotation_speed);
	}


	//Add the new rotation angle and multiply it by the new transform
	void move_unit(){
		//Add thrust to the unit to move it in the target direction.
		rb = transform.GetComponent<Rigidbody2D>();

		//CAUSES A LARGE AMOUNT OF FRAME DROPS WHNEN USER CLICKS ENOUGH.
		if(rb.velocity.magnitude < 1.5){
			Vector3 vel = transform.rotation * Vector3.up;
			rb.AddForce(vel * .1f * Time.deltaTime);
		}

		if(engaging){
			if (Vector3.Distance (transform.position, new Vector3(targeted_unit.transform.position.x,targeted_unit.transform.position.y,0)) < 5f) {
				cur_moving = false;
				engines_off();
			}
		}else{
			//If the unit it close enough to the target then set moving to false
			//and remove the thrust from the unit.
			if (Vector3.Distance (transform.position, new Vector3(movement_target.x,movement_target.y,0)) < 1f) {
				
				if(transform.gameObject.tag.Contains("EnemyUnit")){
					transform.GetComponent<EnemyControl>().wandering = false;
				}

				//Stop moving when close enough and then make the target
				//disapear.
				cur_moving = false;
				arrived_target = true;
				cur_speed = 0;
				engines_off();
				player_control.GetComponent<PlayerControl>().target_redicule.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	//Simply just gives the selected unit a target.
	//For the specified focused point on the map.
	public void set_target(Vector3 target){
		//We want to check if this is a player unit or a CPU enemy
		//Because the wandering function will break.
		if(transform.gameObject.tag.Contains("Pu")){
			//Set the target for the focused unit.
			movement_target = Camera.main.ScreenToWorldPoint(target);
		}else{
			//Set the target for the focused unit.
			movement_target = target;
		}

		StartCoroutine(movement_routine());
	}

	//When the player wants to focus the unit to attack an enemy 
	//then we will set the enemy target which will cause the unit
	//to begin moving towards/orbiting the target unit and firing.
	public void set_enemy_target(GameObject enemy){
		targeted_unit = enemy;
		engaging = true;
		cur_moving = true;
		engines_on();
	}

	//FUNCTIONS FOR ACTUAL COMBAT BELOW.
	//When shot, the unit will take damage to its current health based on the damage of the weapon.
	public void damage_health(int damage){
		if(max_health != null){
			max_health -= damage;

			if(max_health <= 0){
				Destroy(gameObject);
			}
		}
	}

	//These two functions will check if the engines are enabled/disabled
	//and change them depending on the speed of the unit.
	void engines_off(){
		foreach(Transform engine in transform){
			if(engine.tag == "EngineParticle"){
				engine.GetComponent<ParticleSystem>().enableEmission = false;
			}
		}
	}

	void engines_on(){
		foreach(Transform engine in transform){
			if(engine.tag == "EngineParticle"){
				engine.GetComponent<ParticleSystem>().enableEmission = true;
			}
		}
	}

	//Units need to check if they have been hit, if so check what
	//they have been hit by and apply damage or whatever needs
	//to be affected.
	void OnTriggerEnter2D(Collider2D col){
		//Check if the projectiles origin in not equal to the
		//ship that fired it.
		if(col.gameObject.tag != transform.gameObject.tag){
			Destroy(col.gameObject);
			damage_health(5);
		}
	}

	//ROUTINE THAT WILL HANDLE MOVING UNITS SO THAT WE DO NOT HAVE TO KEEP 
	//THE MOVEMENT INFO IN THE UPDATE FUNCTION.  THIS WILL IMPROVE GAME PERFORMANCE.
	IEnumerator movement_routine(){
		//SINCE THIS COROUTINE HANDLES MOVEMENT, WE WANT TO TURN THE ENGINES
		//ON WHEN IT IS STARTED.
		engines_on();
		isMoving = true;
		arrived_target = false;

		//CHECK IF THE UNIT IS MOVING TOWARDS A TARGET.
		//CHECK THE DISTANCE IF IT IS CLOSE ENOUGH THEN WE SET IS MOVING TO FALSE
		//AND END THE COROUTINE.
		while(Vector3.Distance (transform.position, new Vector3(movement_target.x,movement_target.y,0)) > 3f){
			rotate_unit();
			move_unit();
			yield return null;
		}

		//ONCE THE ABOVE WHILE CONDITION IS FALSE, SET THE UNIT TO NOT MOVING
		//AND TURN OFF THE UNITS ENGINE PARTICLE SYSTEMS.
		arrived_target = true;
		isMoving = false;
		engines_off();
		yield return new WaitForSeconds(.5f);
	}
}
