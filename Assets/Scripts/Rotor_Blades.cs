using UnityEngine;
using System.Collections;

public class Rotor_Blades : MonoBehaviour {

	Car_Movement m; Rigidbody r;

	// Use this for initialization
	void Start () {
		m = transform.root.GetComponent<Car_Movement>();
		m.jump_enabled = true;
		r = transform.root.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		transform.Rotate(new Vector3(0,m.ground?m.speed/10:10,0));
		if(!m.ground)
		{
			r.AddForceAtPosition(transform.up*.35f, this.transform.position, ForceMode.Acceleration);
		}
	}
}
