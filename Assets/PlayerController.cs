using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 1;
	private Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2(Input.GetAxisRaw ("Horizontal"),Input.GetAxisRaw ("Vertical"));

		animator.SetInteger ("horizontal", (int)direction.x);
		animator.SetInteger ("vertical", (int)direction.y);

		if (direction.magnitude > float.Epsilon) {
			Vector2 estimatedPos = new Vector2(transform.position.x + direction.x*speed, transform.position.y + direction.y*speed);
			RaycastHit2D hit = Physics2D.Linecast (transform.position, estimatedPos);
			if (hit.collider == null) {
				transform.position = estimatedPos;
			}
		}
	


		if (Input.GetKeyDown (KeyCode.Space)) {
			animator.SetTrigger ("chop");
		}
	}
}
