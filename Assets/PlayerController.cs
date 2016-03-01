using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 1;
	private Animator animator;
	private Vector2 direction = new Vector2(0,0);

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		int horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		int vertical = (int) (Input.GetAxisRaw ("Vertical"));
		if (horizontal > 0) {
			if (direction.x <= 0) {
				animator.SetTrigger ("right");
				direction.x = 1;
				direction.y = 0;
			}
		} else if (horizontal < 0) {
			if (direction.x >= 0) { 
				animator.SetTrigger ("left");
				direction.x = -1;
				direction.y = 0;
			}
		} else if (vertical > 0) {
			if (direction.y <= 0) {
				animator.SetTrigger ("up");
				direction.y = 1;
				direction.x = 0;
			}
		} else if (vertical < 0) {
			if (direction.y >= 0) {
				animator.SetTrigger ("down");
				direction.y = -1;
				direction.x = 0;
			}
		}

		transform.Translate (direction);

		if (Input.GetKeyDown (KeyCode.Space)) {
			animator.SetTrigger ("chop");
		}
	}
}
