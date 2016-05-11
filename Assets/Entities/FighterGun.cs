using UnityEngine;
using System.Collections;

//Since guns on fighter ships do not rotate we are going to want to
//create a different script for their behavior.
public class FighterGun : MonoBehaviour {

	public GameObject weapon;
	private bool firing;

	// Use this for initialization
	void Start () {
		firing = false;
		//Set where the projectile is being fired from
		//by grabbing the parent ships tag.
		weapon.tag = transform.parent.gameObject.tag;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent.gameObject.GetComponent<GenericUnit>().targeted_unit != null && !firing){
			reset_weapon_fire();
			firing = true;
		}
	}

	//Draw our positions for debugging purposes.
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (transform.position,.1f);
	}

	//Fire our weapon is there is a weapon(projectile) set to be fired.
	void fire_weapon(){
		//Make sure we have a weapon before attempting to fire.	
		Instantiate(weapon,transform.position,transform.rotation);
		reset_weapon_fire();
	}

	void reset_weapon_fire(){
		Invoke("fire_weapon",.5f);
	}
}
