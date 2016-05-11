using UnityEngine;
using System.Collections;

public class FighterSquad : MonoBehaviour {

	public string SquadName;

	public GameObject Fighter1;
	public GameObject Fighter2;
	public GameObject Fighter3;
	public GameObject Fighter4;
	public GameObject Fighter5;

	private GameObject[] fighters;

	// Use this for initialization
	void Start () {
		fighters = new GameObject[4];

		fighters[0] = Fighter1;
		fighters[0] = Fighter2;
		fighters[0] = Fighter3;
		fighters[0] = Fighter4;
		fighters[0] = Fighter5;

		display_fighters();
	}

	void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(4, 3, 10));		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Loop through all of the fighters and display the selected unit
	//redicules.
	void display_fighters(){
		for(int i = 0;i < 5;i++){
			
		}
	}
}
