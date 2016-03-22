﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed;
	private float moveVelocity;
	public float jumpHeight;
	
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	private bool doubleJumped;

	private Animator anim;

	public Transform firePoint;
	public GameObject ninjaStar;

	public float shotDelay;
	private float shotDelayCounter;

	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	private Rigidbody2D myRigidBody2D;

	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;

	// Use this for initialization
	void Start () {
		myRigidBody2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();

		gravityStore = myRigidBody2D.gravityScale;
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
		if(Input.GetButtonDown("Jump") && grounded) {
			//myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpHeight);
			Jump ();
		}
		if(Input.GetButtonDown("Jump") && !doubleJumped && !grounded) {
			//myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpHeight);
			Jump ();
			doubleJumped = true;
		}

		moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");
		
		//moveVelocity = 0f;


		if (knockbackCount <= 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, myRigidBody2D.velocity.y);
		} else {
			if(knockFromRight) {
				myRigidBody2D.velocity = new Vector2(-knockback, knockback);
			}

			if(!knockFromRight) {
				myRigidBody2D.velocity = new Vector2(knockback, knockback);
			}

			knockbackCount -= Time.deltaTime;
		}

		anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

		if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} else if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}

		if(Input.GetButtonDown("Fire2")) {
			Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
			shotDelayCounter = shotDelay;
		}

		if (Input.GetButton ("Fire2")) {
			shotDelayCounter -= Time.deltaTime;

			if(shotDelayCounter <= 0) {
				shotDelayCounter = shotDelay;
				Instantiate(ninjaStar, firePoint.position, firePoint.rotation);

			}
		}

		if (anim.GetBool("Sword")) {
			anim.SetBool("Sword", false);
		}

		if (Input.GetButton("Fire1")) {
			anim.SetBool("Sword", true);
		}

		if (onLadder) {
			myRigidBody2D.gravityScale = 0f;

			climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");

			myRigidBody2D.velocity = new Vector2 (myRigidBody2D.velocity.x, climbVelocity);
		}

		if (!onLadder) {
			myRigidBody2D.gravityScale = gravityStore;
		}
	}

	public void Jump() {
		myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpHeight);
	}
}