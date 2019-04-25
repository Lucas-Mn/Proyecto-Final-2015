using UnityEngine;
using System.Collections;

public class Car_Damage : MonoBehaviour {

	public float health;
	Main m;

	void Awake()
	{m = GetComponent<Main> ();}

	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Damage(float amount)
	{
		health -= amount;
		if (health <= 0)
			m.Wreck ();
	}

	void OnCollisionEnter(Collision col)
	{
		foreach(ContactPoint e in col)
			if(e.otherCollider.transform.tag!="ground" && e.otherCollider.transform.tag!="ball" && col.relativeVelocity.magnitude>20)
		{
			health-=col.relativeVelocity.magnitude*.085f;
		}
		if (health <= 0)
			m.Wreck ();
	}
}
