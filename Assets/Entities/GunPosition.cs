using UnityEngine;
using System.Collections;

//Object that will handle where different types of guns can 
//be depending on the given ship.
public class GunPosition : MonoBehaviour {

	//How fast the given position will rotate towards
	//the given target.
	public int rotate_speed;

	//The parent ship of the gun position.
	private GameObject parent_ship;
	public GameObject current_target;

	//Some units such as certain fighter ships will not
	//have turret based weapons so they will not be able to 
	//rotate.
	public bool can_rotate;

	//Each gun position may or may not have a weapon associated with it.
	public GameObject weapon;

	//How ofter the given weapon will fire, we may change this to each specific
	//weapon later.
	public float fire_rate;

	// Use this for initialization
	void Start () {
		//Grab the parent object of our turret so 
		//we know what to aim at.
		parent_ship = transform.parent.gameObject;
	}

	//Draw our positions for debugging purposes.
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (transform.position,1);
	}
	
	// Update is called once per frame
	void Update () {
		//Grab our target if the parent ship has a target.
		//If not the gun will stay the same position it was 
		//before.
		if(parent_ship.GetComponent<GenericUnit>().target != null){
			current_target = parent_ship.GetComponent<GenericUnit>().target;
		}

		//Make sure we have a target and that the weapon is capable of rotating.
		if(current_target != null && can_rotate == true){

			//Get the direction to the target that needs to be turned.
			Vector3 direction_to = current_target.GetComponent<Transform>().position - transform.position;
			
			//Every update we want to rotate if the rotation
			//of the gun is not equal to the rotation of the target.
			//rotate_towards(current_target);

			//Create our new rotation based on the direction.
			float angle = Mathf.Atan2(direction_to.y, direction_to.x) * Mathf.Rad2Deg;

			//Set the new rotation for our gun position.
			transform.rotation = Quaternion.Euler(0, 0, angle + 90f);

			//reset_weapon_fire();

		}
	}

	//Fire our weapon is there is a weapon(projectile) set to be fired.
	void fire_weapon(){
		//Make sure we have a weapon before attempting to fire.	
		Instantiate(weapon,transform.position,transform.rotation);
		reset_weapon_fire();
	}

	void reset_weapon_fire(){
		Invoke("fire_weapon",fire_rate);
	}

	//When we set a target in the player control we tell the unit to set a target which will
	//in turn invoke this function to begin firing projectiles at the given target.
	public void begin_firing(){
		reset_weapon_fire();
	}
}
