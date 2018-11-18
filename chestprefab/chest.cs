using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

    public static chest instance;

    public GameObject open;
    public GameObject close;
    public GameObject coin;

	// Use this for initialization
	void Start () {
        instance = this;

        close.SetActive(true);
        open.SetActive(false);
        coin.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Open()
    {
        close.SetActive(false);
        open.SetActive(true);
        coin.SetActive(true);
        Destroy(coin, 1);

        movement.instance.silverAdd();

        Debug.Log("Open");
    }
}
