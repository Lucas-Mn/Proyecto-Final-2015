using UnityEngine;
using System.Collections;

public class Turbine : MonoBehaviour {

	public Car_Movement m; ParticleSystem p;

	// Use this for initialization
	void Start () {
		m = transform.root.gameObject.GetComponent<Car_Movement>();
		p = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	if(m.boosting)
		{
			p.emissionRate=300;
			p.startSize=1f*(m.boost_amount/m.boost_max);
		}
		else p.emissionRate=0;
	}
}
