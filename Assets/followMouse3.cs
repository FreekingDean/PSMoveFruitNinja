using UnityEngine;
using System.Collections;
using System;
/* okay so i reattached everything and hopefuly it will stay, i re worked the physics so non on the sides go directly up,
 * i deleted afew unneeded things so please dont merge the whole folder or loading will be painful,
 * i added a second cursor with a seprate script it only works for the mouse and can only slice when the left button is down,
 * so the original cursor and the gui one only work with the move now as i dont know the code please edit this script
 * so that the cursor is hidden unless being used (idk if enable=false in the begining will let it turn its self on, if not just move it out of sight while out of use)
 * i also need you to set it to only slice when a button is down, the code for mouse down is comented out below,
 * i edited the volume so it is more even between the songs, and i need you to do a gui score and something for the snowman, points are in newbehaviorscript,
 * i changed the launcher so it is random what and howmany show up,also i fell like the music could be reworked or a longer version as the jump in the loop is noticable,
 and if you have time pressing 'p' to go to a pause screen or a countdown before the round would be cool, also sounds for slicing too. other then the move related stuff
 i plan to work in these things to this is just a general list of what could still be done*/
public class followMouse3 : MonoBehaviour {
    private PSMoveSharpState state;

    private int length, height;

	// Use this for initialization
	void Start () {
        length = Screen.width;
        height = Screen.height;
	}
    void Update()
    {
        float height = transform.position.y;
        float width = transform.position.x;

        float posX=0;
        float posY=0;
		bool  isTrigger = false;
		
		
      if (MainMenuGui.isCalibrated2)
      {
            state = MoveMeConnect.getState();
			
			posX = state.pointerStates[1].normalized_x;
            posY = state.pointerStates[1].normalized_y;

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
		UInt16 just_pressed = (UInt16)(state.gemStates[1].pad.digitalbuttons);

        const int PadTick = 1 << 2;
			
        if ((just_pressed & PadTick) == PadTick)
				transform.collider.isTrigger = false;  
		else 
				transform.collider.isTrigger = true;
               
        }
		
		if(ScoreKeeper.getPlayers() == 0 | ScoreKeeper.getPlayers() == 1 | ScoreKeeper.getPlayers() == 2)
		{
			transform.collider.isTrigger = false;
			transform.position =  new Vector3(0, 0, 100);
		}
		else{
	        if ((Convert.ToSingle(-15 + (posX / (Screen.width / 100)) * .3) < -18))
	        {
	            width = -18;
	        }
	        else if (Convert.ToSingle(-15 + (posX / (Screen.width / 100)) * .3) > 18)
	        {
	            width = 18;
	        }
	        else
	        {
	            width = Convert.ToSingle(-15 + (posX / (Screen.width / 100)) * .3);
	        }
	        if ((Convert.ToSingle(-15 + (posY / (Screen.height / 100)) * .3) < -13))
	        {
	            height = -13;
	        }
	        else if (Convert.ToSingle(-15 + (posY / (Screen.height / 100)) * .3) > 13)
	        {
	            height = 13;
	        }
	        else
	        {
	            height = Convert.ToSingle(-15 + (posY / (Screen.height / 100)) * .3);
	        }
	
	        transform.position = new Vector3(width, height, -3);//new Vector3(18,0,-3);
		}

    }
}