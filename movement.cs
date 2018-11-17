using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    Transform transf;

	// Use this for initialization
	void Start () {
        transf = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.W))
        {
            transf.Translate(0, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transf.Translate(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transf.Translate(0, -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transf.Translate(1, 0, 0);
        }


    }
}
