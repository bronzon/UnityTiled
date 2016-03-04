using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	
	void Start (){
	    GameObject player = GameObject.FindGameObjectWithTag("Player");
	    player.transform.position = transform.position;
	}
	
}
