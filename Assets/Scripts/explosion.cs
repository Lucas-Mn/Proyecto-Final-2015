using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	public float damage, timer;

	// Use this for initialization
	void Start () {
		timer=.5f;
	}
	
	// Update is called once per frame
	void Update () {
		timer-=Time.deltaTime;
		if(transform.localScale.x<6)
			transform.localScale+=Vector3.one*.3f;
	}

	void OnTriggerEnter(Collider other)
	{
		if(timer>0 && damage > 0 && other.transform.root.tag=="kart")
		{
			other.transform.root.gameObject.GetComponent<Car_Damage>().Damage(100);
		}
	}
}
