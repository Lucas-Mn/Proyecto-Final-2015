using UnityEngine;
using System.Collections;

public class Timed_Destroy : MonoBehaviour {

	float timer;

	// Use this for initialization
	void Start () {
		timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
		timer-=Time.deltaTime;
		if(timer<0)
			Destroy(this.gameObject);
	}


}
