using UnityEngine;
using System.Collections;

public class Test_Force : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<Rigidbody>().AddForce(transform.forward*10000*Input.GetAxis("Vertical"));
		GetComponent<Rigidbody>().AddTorque(transform.up*1000*Input.GetAxis("Horizontal"));
	}
}
