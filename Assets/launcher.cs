using UnityEngine;
using System.Collections;
using System.Threading;

public class launcher : MonoBehaviour {

    public GameObject watermellon;
    public GameObject pear;
    public GameObject orange;
    public GameObject coconut;
    public GameObject lemon;
    public GameObject snowman;
    float time = 0;
    int round = 0;
    int prob=24;
    bool pause = false;
    float oldtime = 0;
    float newtime = 0;
    float settime = 0;
	float roundTime = 10;
	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
	}
    void spawn(int fruitnum)
    {
        bool fool = true;
        GameObject fruit;
        switch (fruitnum)
        {
            case 1:
            case 7:
                fruit = watermellon;
                break;
            case 2:
            case 8:
            case 12:
                fruit = pear;
                break;
            case 3:
            case 9:
            case 13:
                fruit = orange;
                break;
            case 4:
                fruit = coconut;
                break;
            case 5:
            case 10:
            case 14:
                fruit = lemon;
                break;
            case 6:
                fruit = snowman;
                break;
            default:
                fruit = snowman;
                fool = false;
                break;
        }
        Vector3 spawn = new Vector3(0, 0, 13);
        if(fool!=false)
            Instantiate(fruit, spawn, transform.rotation);

    }
	
	void OnGUI()
	{
		if(pause)
		{
			GUI.skin.label.normal.textColor = Color.black;
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			float roundTimeNew = Mathf.Round(roundTime);
			GUI.Label(new Rect(0,0,Screen.width, Screen.height), roundTimeNew.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
        prob = 24;
        oldtime = oldtime+newtime;
        newtime = Time.deltaTime;
        time = Mathf.Round(oldtime + newtime);
		
        if (time > settime)
        {
			roundTime -= newtime;
			pause = true;
            if (time > (10 + settime))
            {
                settime = 3 * Mathf.Pow(2, round);
                oldtime = 0;
                time = 0;
                round++;
				roundTime = 10;
				ScoreKeeper.moveScore();
				pause = false;
            }
        }
        else if(Mathf.Round(oldtime+1)==time)
        {
            if (round <= 10)
                prob = prob - round;
            else
                prob = 14;

            spawn(UnityEngine.Random.Range(1, prob));
            spawn(UnityEngine.Random.Range(1, prob));
            spawn(UnityEngine.Random.Range(1, prob));
            spawn(UnityEngine.Random.Range(1, prob));
            spawn(UnityEngine.Random.Range(1, prob));
            spawn(UnityEngine.Random.Range(1, prob));
        }	
	}
}
