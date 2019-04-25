using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	public float force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
//		if(other.transform.root.tag=="kart")
//		{
			other.attachedRigidbody.AddForce(this.transform.forward*force);
//		}
	}
}
