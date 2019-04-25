using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	Race race;
	public bool[] has_id;
	public int number;

	void Start () 
	{
		race = this.GetComponentInParent<Race>();
	}
	

	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.root.tag=="kart")
		{
			race.Check(int.Parse(other.transform.root.name.Substring(5))-1, number);//get ID from object name
		}
	}
}
