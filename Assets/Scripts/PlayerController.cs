using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed;
	public float jumpHeight;
	
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	private bool doubleJumped;
	
	private Rigidbody2D myRigidBody2D;

	private Animator anim;
	
	// Use this for initialization
	void Start () {
		myRigidBody2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}
	
	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update () {
		if (grounded) {
			doubleJumped = false;
		}
		anim.SetBool ("Grounded", grounded);
		if(Input.GetKeyDown (KeyCode.W) && grounded) {
			//myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpHeight);
			Jump ();
		}
		if(Input.GetKeyDown (KeyCode.W) && !doubleJumped && !grounded) {
			//myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpHeight);
			Jump ();
			doubleJumped = true;
		}
		if(Input.GetKey (KeyCode.D)) {
			myRigidBody2D.velocity = new Vector2(moveSpeed, myRigidBody2D.velocity.y);
		}
		if(Input.GetKey (KeyCode.A)) {
			myRigidBody2D.velocity = new Vector2(-moveSpeed, myRigidBody2D.velocity.y);
		}

		anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
		if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} else if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
	}

	public void Jump() {
		myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpHeight);
	}
}