using UnityEngine;
using System.Collections;
using System;

public class followmouse2 : MonoBehaviour {
    private int length, height;
	// Use this for initialization
	void Start () {
        length = Screen.width;
        height = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            transform.collider.isTrigger = false;
        if (Input.GetMouseButtonUp(0))
            transform.collider.isTrigger=true;

        float height = transform.position.y;
        float width = transform.position.x;

        float posX = Input.mousePosition.x;
        float posY = Input.mousePosition.y;
		
		if(ScoreKeeper.getPlayers() == 1 || ScoreKeeper.getPlayers() == 3)
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
	
	        transform.position = new Vector3(width, height, -3);
		}
	}
}