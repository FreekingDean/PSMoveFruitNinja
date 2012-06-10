using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	
	// Update is called once per frame
	void OnGUI(){
		GUI.skin.box.fontSize = 36;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		string output = "This Round : " + ScoreKeeper.getScore() + "\n" + "Total Score: " + ScoreKeeper.getTotal();
		GUI.Box(new Rect(0,0,350, Screen.height), output);
		
		if(GUI.Button(new Rect(0, Screen.height  - 100, 350, 100), "Back To Main Menu"))
			Application.LoadLevel(0);
	}
}