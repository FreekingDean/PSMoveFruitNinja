using UnityEngine;
using System.Collections;
using System;

public class moveupcredits : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0,-45, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0,Convert.ToSingle(transform.position.y + .1), 0);
        if (transform.position.y > 50)
        {
            Destroy(gameObject);
			Application.LoadLevel(0);
        }
	}
}
