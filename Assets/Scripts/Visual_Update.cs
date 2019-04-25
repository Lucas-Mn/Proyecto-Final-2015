using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Visual_Update : MonoBehaviour 
{
	
	public GameObject wheels_object;
	public GameObject RR_wheel, RL_wheel, FR_wheel, FL_wheel, m_rr, m_rl, m_fr, m_fl;
	public GameObject body, spoiler, front;
	Main main;
	Car_Movement m;
	Parts parts;
	Rigidbody r;
	axles wheels;
	WheelCollider FR, FL, RR, RL;
	ParticleSystem PSL, PSR, PSB;


	// Use this for initialization
	void Start () 
	{	
		main = GetComponent<Main>();
		m = GetComponent<Car_Movement>();
		r = main.GetComponent<Rigidbody>();
		parts = GameObject.Find ("Parts").GetComponent<Parts>();
		Set_Stuff();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(main.wheel.standard)
		{
		#region "Wheels"
		set_wheel(RR,RR_wheel,m_rr, false, false);
		set_wheel(RL,RL_wheel,m_rl, true, false);
		set_wheel(FR,FR_wheel,m_fr, false, true);
		set_wheel(FL,FL_wheel,m_fl, true, true);
		#endregion
		}
		else set_mid_wheels();

		PSL.Emit((int)(4.2f*m.slip)); PSR.Emit((int)(4.2f*m.slip));


	}


	int timer_smoke=0;
	void FixedUpdate()
	{	
		float h = main.health;
		float mh = main.max_health;
		timer_smoke++;
		if(h/mh < .25f) PSB.startColor = new Vector4(0, 0, 0, 255); 
		else PSB.startColor = new Vector4(255, 255, 255, 255); 
		if(timer_smoke>h/mh*350 && mh-h > 10)
		{PSB.Emit((int)(1)); timer_smoke=0;}
	}

	public void Set_Stuff()
	{
		if(main.wheel.standard)
		{
		#region wheels
		wheels = GetComponent<Main> ().wheels;
		FR = wheels.FR.GetComponent<WheelCollider> ();
		FL = wheels.FL.GetComponent<WheelCollider> ();
			RR = wheels.RR.GetComponent<WheelCollider> (); PSR = RR.transform.FindChild("PS").GetComponent<ParticleSystem>();
			RL = wheels.RL.GetComponent<WheelCollider> (); PSL = RL.transform.FindChild("PS").GetComponent<ParticleSystem>();

			PSB = transform.FindChild("body").GetComponent<ParticleSystem>();
		
		RR_wheel = GameObject.Instantiate(Resources.Load("Wheel "+main.id_wheels)) as GameObject; RR_wheel.transform.Rotate(180,0,0);
			m_rr = RR_wheel.transform.FindChild("model").gameObject;
		RL_wheel = GameObject.Instantiate(Resources.Load("Wheel "+main.id_wheels)) as GameObject;
			m_rl = RL_wheel.transform.FindChild("model").gameObject;
		FR_wheel = GameObject.Instantiate(Resources.Load("Wheel "+main.id_wheels)) as GameObject; FR_wheel.transform.Rotate(180,0,0);
			m_fr = FR_wheel.transform.FindChild("model").gameObject;
		FL_wheel = GameObject.Instantiate(Resources.Load("Wheel "+main.id_wheels)) as GameObject;
			m_fl = FL_wheel.transform.FindChild("model").gameObject;
		
		RR_wheel.transform.parent = transform;
		RL_wheel.transform.parent = transform;
		FR_wheel.transform.parent = transform;
		FL_wheel.transform.parent = transform;
		#endregion
		}
		else
		{
			wheels = GetComponent<Main> ().wheels;
			FR = wheels.FR.GetComponent<WheelCollider> ();
			FL = wheels.FL.GetComponent<WheelCollider> ();
			RR = wheels.RR.GetComponent<WheelCollider> ();
			RL = wheels.RL.GetComponent<WheelCollider> ();

			FR_wheel = GameObject.Instantiate(Resources.Load("Wheel "+main.id_wheels)) as GameObject; FR_wheel.transform.Rotate(0,0,0);
			FL_wheel = GameObject.Instantiate(Resources.Load("Wheel "+main.id_wheels)) as GameObject; FL_wheel.transform.Rotate(main.wheel.inversion);

//			FR_wheel.transform.parent=transform; FR_wheel.transform.localPosition=main.body.mid_wheel;
//			FL_wheel.transform.parent=transform; FL_wheel.transform.localPosition=new Vector3(-main.body.mid_wheel.x, main.body.mid_wheel.y, main.body.mid_wheel.z);

			FR_wheel.transform.parent=transform; 
			FL_wheel.transform.parent=transform;
		}

		body = GameObject.Instantiate (Resources.Load ("Body " + main.id_body)) as GameObject; body.name = "body";
		body.transform.parent = transform.FindChild("body"); body.transform.localPosition = (main.body.correction);
		
		spoiler = GameObject.Instantiate (Resources.Load ("spoiler " + main.id_spoiler)) as GameObject;
		spoiler.transform.parent = transform.FindChild("body"); spoiler.name = "spoiler";
		spoiler.transform.localPosition = main.body.spoiler_addon+parts.spoiler_addon[main.id_spoiler].pos_correction;
		main.gun [1].Set (main.spoiler.gun_name);

		front = GameObject.Instantiate (Resources.Load ("Front " + main.id_front)) as GameObject;
		front.transform.parent = transform.FindChild ("body"); front.name = "front";
		front.transform.localPosition = main.body.front_addon+parts.front_addon [main.id_front].pos_correction;
		main.gun [0].Set (main.front.gun_name);
	}

	void set_wheel(WheelCollider collider, GameObject visualWheel, GameObject model, bool left, bool front)
	{
		
		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);

		visualWheel.transform.position = position;

		if (front) {
			visualWheel.transform.localRotation = Quaternion.Euler (new Vector3 (0, 30 * m.out_horizontal, 270));
			if (!left)
				visualWheel.transform.Rotate (main.wheel.inversion);

		} else 
		{
			visualWheel.transform.localRotation = Quaternion.Euler(new Vector3(0,0,270));
			if(!left)visualWheel.transform.Rotate(main.wheel.inversion);
		}

		model.transform.Rotate(new Vector3(0, 0, left ? -m.speed : m.speed*.7f));

	}

	void set_mid_wheels()
	{
		Vector3 pos_1, pos_2; Quaternion rot; FR.GetWorldPose(out pos_1, out rot); RR.GetWorldPose(out pos_2, out rot);
		FR_wheel.transform.position=Vector3.Lerp(pos_1, pos_2, .5f);
		FL.GetWorldPose(out pos_1, out rot); RL.GetWorldPose(out pos_2, out rot);
		FL_wheel.transform.position=Vector3.Lerp(pos_1, pos_2, .5f);
	}

	void OnCollisionEnter(Collision col)
	{
		foreach(ContactPoint e in col)
			if(e.otherCollider.transform.tag!="ground")
			Instantiate(Resources.Load("sparks"), e.point, new Quaternion());
	}
}
