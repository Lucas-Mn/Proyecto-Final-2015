using UnityEngine;
using System.Collections;

public class Cannon_Ball : MonoBehaviour {

	public float arming, damage, explosion_damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		arming+=Time.deltaTime;
		if(arming>1)
		{
			GameObject x = Instantiate(Resources.Load ("Explosion"), transform.position, transform.rotation) as GameObject;
			x.GetComponent<explosion>().damage=explosion_damage;
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
	if(arming>.1f && other.transform.root.tag=="kart")
	{
		GameObject x = Instantiate(Resources.Load ("Explosion"), transform.position, transform.rotation) as GameObject;
		x.GetComponent<explosion>().damage=explosion_damage;
		other.transform.root.gameObject.GetComponent<Car_Damage>().Damage(damage);
		GameObject.Destroy(this.gameObject);
	}
	}
}
