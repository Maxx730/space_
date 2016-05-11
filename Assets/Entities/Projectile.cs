using UnityEngine;
using System.Collections;

//Generic object for weapon shots
public class Projectile : MonoBehaviour {

	//Speed of the actual projectile shot.
	public int projectile_speed;
	private int range;
	private string parent_type;

	//Keep track of which ship fired the projectile.
	public GameObject origin;


	void Start(){
		
	}

	void Update(){
		Quaternion rot = transform.rotation;
		float z = rot.eulerAngles.z;
		rot = Quaternion.Euler(0,0,z);
		Vector3 vel = new Vector3(0,(projectile_speed*Time.deltaTime),0);
		transform.position += rot * vel;
	}

}