﻿using UnityEngine;
using System.Collections;

public class suc_ctrl : MonoBehaviour {

	
	private Animator anim;
	private CharacterController controller;
	private bool battle_state;
	public float speed = 6.0f;
	public float runSpeed = 1.7f;
	public float turnSpeed = 60.0f;
	public float gravity = 17.0f;
	public int suc_class; // 1-warrior 2-shamon 3-archer
	private Vector3 moveDirection = Vector3.zero;
	

	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
				
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
		if (Input.GetKey("2")) //idle
		{
			anim.SetInteger("battle", 1);
			battle_state = true;
			//gravity = 0;
			
		}
		if (Input.GetKey("1")) 			//battle/fly_state
		{
			anim.SetInteger("battle", 0);
			battle_state = false;
			//gravity = 17;
		}
		if (Input.GetKey ("up")) {		 //moving
			if (battle_state == false)
			{
				anim.SetInteger ("moving", 1);//walk
				runSpeed = 1.0f;
			} else 
			{
				anim.SetInteger ("moving", 2);//fly
				runSpeed = 2.6f;
			}

			
		} else {
			anim.SetInteger ("moving", 0);
		}

		if (Input.GetKey ("down")) {

			runSpeed = 1.0f;			
			anim.SetInteger ("moving", 3);//fly backward
		}
		
		if (Input.GetKeyDown("o")) // die1
		{
			anim.SetInteger("moving", 14);
		}
		if (Input.GetKeyDown("i")) // die2
		{
			anim.SetInteger("moving", 15);
		}
		
		if (Input.GetKeyDown("u")) // hit
		{
			int n = Random.Range(0,2);
			if (n == 0) {
				anim.SetInteger("moving", 12);
			} else {anim.SetInteger("moving", 13);}
		}
		
		
		
		if (Input.GetKeyDown("p")) // defence_start
		{
			anim.SetInteger("moving", 9);
		}
		if (Input.GetKeyUp("p")) // defence_end
		{
			anim.SetInteger("moving", 10);
		} 
		if (Input.GetKeyDown ("l")) // kick
		{
			anim.SetInteger("moving", 11);
		}
		
		
		switch (suc_class) {
		case 1: 
			if (Input.GetMouseButtonDown (0)) // attack1
			{
				anim.SetInteger("moving", 4);
			}
			if (Input.GetMouseButtonDown (1)) // attack2
			{
				anim.SetInteger("moving", 5);
			}
			if (Input.GetMouseButtonDown (2)) //power atack
			{
				anim.SetInteger("moving", 6);
			}
			
			break;
			
		case 2:
			if (Input.GetMouseButtonDown (0)) // cast 1
			{
				anim.SetInteger("moving", 7);
			}
			if (Input.GetMouseButtonDown (1)) // cast 2
			{
				anim.SetInteger("moving", 8);
			}
			if (Input.GetMouseButtonDown (2)) // attack
			{
				anim.SetInteger("moving", 6);
			}
			
			
			break;
			
			
		}
		
		
		
		if(controller.isGrounded)
		{
			moveDirection=transform.forward * Input.GetAxis ("Vertical") * speed * runSpeed;
			
		}
		float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move(moveDirection * Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;
		
		
		
	}
}
