using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public float arming;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		arming+=Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if(arming>1)
		if(other.transform.root.tag=="kart")
		{
			Instantiate(Resources.Load ("Explosion"), transform.position, transform.rotation);
			other.transform.root.gameObject.GetComponent<Car_Damage>().Damage(100);
			GameObject.Destroy(this.gameObject);
		}
	}
}
