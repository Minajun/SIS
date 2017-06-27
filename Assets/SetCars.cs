using UnityEngine;
using System.Collections;

public class SetCars : MonoBehaviour {
	public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int r = (int)Random.Range (0,100);
		if(r < 8){
			Instantiate (obj);
		}
	}
}
