using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	public float grip_multiplier;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		GameObject kart = other.transform.root.gameObject;
		kart.GetComponent<Main> ().Set_Grip (grip_multiplier);
	}
}
