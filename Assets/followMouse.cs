using UnityEngine;
using System.Collections;
using System;

/**
 * This is the default class that depicts how to Move the coursor to follow the mouse.
 * 
 * The playerNumber is set in the Unity Editor itself
 */

public class followMouse : MonoBehaviour {
    private PSMoveSharpState state;
	
	public int playerNumber; //Coursor Number
	
	private bool isNotPlaying; //checks if cursor is playing in game or not.
	
	float height; //
    float width;

    float posX;
    float posY;
	bool  isTrigger;
	
	// Use this for initialization
	void Start () {
        isNotPlaying = true;
		height = transform.position.y;
        width = transform.position.x;

        posX=0;
        posY=0;
		isTrigger = false;
	}
	
    void Update()
    {
		if ((playerNumber == 1 || playerNumber == 2) && MoveMeConnect.client_connected)
		{       
			getPosition(true);
       	}
       	else
       	{
			getPosition(false);
		}	
		
		checkPlayingStatus();
		
		if(isNotPlaying)
			hideCursor();
		else
			moveCursor();
    }
	
	/**
	 * Checks to see if the cursor is a move controller then returns
	 * the position of the cursor. It gets the move controller's pointer
	 * position or mouse's position on screen. Also checks to see if the
	 * left mouse button or move button is pressed down.
	 * 
	 * bool isMove - checks if getting move controller or mouse
	 */
	private void getPosition(bool isMove)
	{
		if(isMove)
		{
			state = MoveMeConnect.getState();
			
			posX = state.pointerStates[0].normalized_x;
            posY = state.pointerStates[0].normalized_y;

            int sLength = Screen.width;
            int sHeight = Screen.height;

            if (posX > 0)
                posX = (sLength * posX) + (sLength / 2);
			
            if (posX < 0)
                posX = (sLength / 2) - (-sLength * posX);
			
            if (posX == 0)
                posX = sLength / 2;
			
            if (posY > 0)
                posY = (sHeight * posY) + (sHeight / 2);
			
            if (posY < 0)
                posY = (sHeight / 2) - (-sHeight * posY);
			
            if (posY == 0)
                posY = sHeight / 2;
			
			UInt16 just_pressed = (UInt16)(state.gemStates[0].pad.digitalbuttons); //Gets the last pressed move button

        	const int PadTick = 1 << 2;
			
        	if ((just_pressed & PadTick) == PadTick)
				transform.collider.isTrigger = false;  
			else 
				transform.collider.isTrigger = true;
		}
		else
		{
			posX = Input.mousePosition.x;
        	posY = Input.mousePosition.y;
        	
        	if (Input.GetMouseButtonDown(0))
            	transform.collider.isTrigger = false;
        	if (Input.GetMouseButtonUp(0))
            	transform.collider.isTrigger = true;
		}
	}
	
	/**
	 * Checks the status of the current cursor to see if it is play
	 * or not. Adjusts the isNotPlaying bool variable correctly
	 */
	public void checkPlayingStatus()
	{
		if(ScoreKeeper.getPlayers() == 2)
			if(playerNumber == 0 || playerNumber == 1)
				isNotPlaying = false;
		
		if(ScoreKeeper.getPlayers() == 3)
			if(playerNumber == 1 || playerNumber == 2)
				isNotPlaying = false;
		
		if(ScoreKeeper.getPlayers() == playerNumber || ScoreKeeper.getPlayers() == 4)
			isNotPlaying = false;
	}
	
	/**
	 * Hides the cursor in order to keep it out of play
	 */
	public void hideCursor()
	{
		transform.collider.isTrigger = false;
		transform.position =  new Vector3(0, 0, 100);
	}
	
	/**
	 * Moves the cursor to a position on the screen
	 * where it does not go out of play
	 */
	public void moveCursor()
	{
		if ((Convert.ToSingle(-15 + (posX / (Screen.width / 100)) * .3) < -18))
			width = -18;
	    else if (Convert.ToSingle(-15 + (posX / (Screen.width / 100)) * .3) > 18)
			width = 18;
	    else
			width = Convert.ToSingle(-15 + (posX / (Screen.width / 100)) * .3);
		
		if ((Convert.ToSingle(-15 + (posY / (Screen.height / 100)) * .3) < -13))
			height = -13;
	    else if (Convert.ToSingle(-15 + (posY / (Screen.height / 100)) * .3) > 13)
	        height = 13;
	    else
	        height = Convert.ToSingle(-15 + (posY / (Screen.height / 100)) * .3);
	    
		transform.position = new Vector3(width, height, -3);//new Vector3(18,0,-3);
	}
}