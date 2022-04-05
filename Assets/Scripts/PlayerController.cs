using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
	private InputActions InputActions;   

    public float speed = 0;
    public float jumpSpeed = 0;
    public int jumpCounter = 0;                     //setting up a counter for my double jump
 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
        
	private Rigidbody rb;
	private int count;
	private float movementX;
	private float movementY;
	


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
		Vector2 movementVector = movementValue.Get<Vector2>();

		movementX = movementVector.x;
		movementY = movementVector.y;
		
    }
    
   
    void OnJump(InputValue movementValue)
    {
    	// Debug.Log("Jumping");                                           //used this to test if Im registering the spacebar
    	if (jumpCounter <= 1)                                              //if the jump counter is 0 or 1 
    	{
    		rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);        //we do the jump using Vector3.up 
    		jumpCounter += 1;                                              //and update the counter
    	}
    }	

    void SetCountText()
    {
    	countText.text = "Count: " + count.ToString();
		if(count >= 12)
		{
			winTextObject.SetActive(true);
		}    	
    }

    void FixedUpdate()
    {
    	Vector3 movement = new Vector3(movementX, 0.0f, movementY);
    	
    	rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
    	if(other.gameObject.CompareTag("PickUp"))
    	{
    		other.gameObject.SetActive(false);	
    		count += 1;

    		SetCountText();
    	}
    	
    	if(other.gameObject.CompareTag("Ground"))                          //this is where I test for collision with the ground
    	{	 
    		jumpCounter = 0;                                               //if collision happened resetting the jump counter
    	}	
    }                                                                      //basically this was a way to reset the double jump once you hit the ground. 

}
