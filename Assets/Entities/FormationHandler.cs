using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FormationHandler : MonoBehaviour {

	public GameObject target_red;
	public List<GameObject> targets = new List<GameObject>();

	//WHEN A PLAYER UNIT IS SELECTED AND THE USER CLICK ON A TARGET
	//ADD TARGET UNITS TO DISPLAY THE TARGET DESTINATION AND SEND
	//THE UNITS TO THEIR GIVEN CALCULATED POINTS.
	public void set_movement_formation(Vector3 origin_tar){
		int unit_amount = transform.GetComponent<UnitGroup>().units.Count;
		Vector3 newTar = Camera.main.ScreenToWorldPoint(origin_tar);
		int i = 0;
		int toLeft = 0;
		int toRight = 0;
		int leftOffset = 1;
		int rightOffset = 1;

		foreach(Transform unit in transform.GetComponent<UnitGroup>().units){
			if(i == 0){
				targets.Add((GameObject)Instantiate(target_red,new Vector3(newTar.x,newTar.y,-57.7f), transform.rotation) as GameObject);
				unit.GetComponent<GenericUnit>().set_target(new Vector3(newTar.x,newTar.y,-57.7f));
				i++;
			}else{
				if(toLeft < toRight || toLeft == toRight){
					targets.Add((GameObject)Instantiate(target_red,new Vector3(newTar.x - leftOffset,newTar.y,-57.7f), transform.rotation) as GameObject);
					unit.GetComponent<GenericUnit>().set_target(new Vector3(newTar.x - leftOffset,newTar.y,-57.7f));
					leftOffset++;
					toLeft++;
					i++;
				}else{
					targets.Add((GameObject)Instantiate(target_red,new Vector3(newTar.x + rightOffset,newTar.y,-57.7f), transform.rotation) as GameObject);
					unit.GetComponent<GenericUnit>().set_target(new Vector3(newTar.x + rightOffset,newTar.y,-57.7f));
					rightOffset++;
					toRight++;
					i++;
				}
			}
		}
	}

	//WHEN A USER CLICKS IF THERE ARE TARGETS ALREADY WE WANT TO CLEAR THE TARGETS AND THEN
	//RESET THEM TO THE NEW DESTINATION.
	public void clear_formation_targets(){
		if(targets.Count != 0){
			foreach(GameObject target in targets){
				GameObject.Destroy(target);
			}
		}else{
			return;
		}
	}
}
