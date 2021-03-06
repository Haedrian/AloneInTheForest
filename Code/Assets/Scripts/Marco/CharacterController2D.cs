﻿using System.Collections;
using UnityEngine;
using System;
using System.Linq;

public class CharacterController2D : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;			// For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.
    [HideInInspector]
    public bool isMoving = false;

    public float moveForce = 365f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
    public float jumpForce = 1000f;			// Amount of force added when the player jumps.


    private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.


	Rigidbody2D rigidBody;
	
	public KeyCode[] Instructions = null;
	
	public int InstructionNumber = 0;

    void Awake()
    {
		//Read the instructions
		string instructionString = PlayerPrefs.GetString ("Instructions");
		
		Instructions = instructionString.Split(',').Select(i => (KeyCode) Enum.Parse(typeof(KeyCode),i)).ToArray();

        rigidBody = this.transform.GetComponent<Rigidbody2D>();
        if (rigidBody == null)
            throw new MissingComponentException();
    }




    void Update()
    {
      
    }

    void FixedUpdate()
    {
		KeyCode currentInstruction = KeyCode.X;

		try 
		{
			currentInstruction = Instructions [++InstructionNumber];
		}
		catch(Exception ex)
		{

		}

		if (currentInstruction == null)
		{

		}
		else

		// If the jump button is pressed and the player is grounded then the player should jump.
		if (currentInstruction == (KeyCode.E) && rigidBody.velocity.y == 0)
		{
			jump = true;
		}

		isMoving = currentInstruction == KeyCode.W;

        int force = System.Convert.ToInt32(isMoving);

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (force * rigidbody2D.velocity.x < maxSpeed)
            // ... add a force to the player.
            rigidbody2D.AddForce(Vector2.right * force * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (force > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (force < 0 && facingRight)
            // ... flip the player.
            Flip();

        // If the player should jump...
        if (jump)
        {
            // Add a vertical force to the player.
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}