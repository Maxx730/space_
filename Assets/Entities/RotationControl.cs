using UnityEngine;
using System.Collections;

public class RotationControl : MonoBehaviour {

	//Every gameobject needs to have a target object to rotate around
	//such as a sun or planet or even asteroid.
	public GameObject target;
	//Controls how fast around the target this object
	//will orbit.
	public int orbit_speed;
	public float orbitDistance;
	
	// Update is called once per frame
	void Update () {
		//Make sure the current object actually has a target to
		//rotate around before begging any kind of animation.
		if(target != null){
			// Keep us at orbitDistance from target
			transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
			transform.RotateAround (target.transform.position, new Vector3(1,1,-15), orbit_speed * Time.deltaTime);
		}	
	}
}
