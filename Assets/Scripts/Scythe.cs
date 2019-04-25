using UnityEngine;
using System;
using System.Collections;

public class Scythe : MonoBehaviour {

	ParticleSystem p;

	// Use this for initialization
	void Start () {
		p=GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider other)
	{
		if(other.transform.root.tag=="kart")
		{
			p.Emit(1);
			other.transform.root.gameObject.GetComponent<Car_Damage>().Damage(100*Time.deltaTime);
		}
	}
}
