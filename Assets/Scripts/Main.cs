using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public bool random, custom, set, display;
	public Parts parts; 
	public axles wheels;
	public float final_steer_force, steer_limit;
	public Body body; public Wheel wheel; public Addon spoiler, front;
	public Gun[] gun;
	WheelCollider[] w; Player_Control pc;
	Visual_Update visuals;
	Car_Movement m; public Car_Damage damage;
	Race race;

	//parts
	public int id_body; public int id_wheels; public int id_spoiler, id_mid, id_front;

	public GameObject body_visual;

	//debug
	public float debug_health, max_health;

	void Awake()
	{
		pc = GetComponent<Player_Control>();
		if (random) {
			id_body = Random.Range (0, 5);
			id_wheels = Random.Range (0, 4);
			id_front = Random.Range (0, 3);
			id_spoiler = Random.Range (0, 4);
		} 
		else if (custom) {
			LoadCar lc = GameObject.Find ("CarObject").GetComponent<LoadCar>();
			switch(pc.ID)
			{
			case 1:
				id_body = lc.player1Parts[0];
				id_wheels = lc.player1Parts[1];
				id_front = lc.player1Parts[2];
				id_spoiler = lc.player1Parts[3];
				break;
			case 2:
				id_body = lc.player2Parts[0];
				id_wheels = lc.player2Parts[1];
				id_front = lc.player2Parts[2];
				id_spoiler = lc.player2Parts[3];
				break;
			case 3:
				id_body = lc.player3Parts[0];
				id_wheels = lc.player3Parts[1];
				id_front = lc.player3Parts[2];
				id_spoiler = lc.player3Parts[3];
				break;
			case 4:
				id_body = lc.player4Parts[0];
				id_wheels = lc.player4Parts[1];
				id_front = lc.player4Parts[2];
				id_spoiler = lc.player4Parts[3];
				break;
			}
		}
		body = GameObject.Find ("Parts").GetComponent<Parts>().body[id_body];
		wheel = GameObject.Find ("Parts").GetComponent<Parts> ().wheel [id_wheels];
		spoiler = GameObject.Find ("Parts").GetComponent<Parts> ().spoiler_addon [id_spoiler]; spoiler.name = "spoiler";
		front = GameObject.Find ("Parts").GetComponent<Parts> ().front_addon [id_front];
		m = GetComponent<Car_Movement> (); damage = GetComponent<Car_Damage> ();

		visuals = GetComponent<Visual_Update> ();

		if(!display)
		race=GameObject.Find(GameObject.Find("Game").GetComponent<Game>().track).GetComponentInChildren<Race>();

	}
	void Start () 
	{
#region stats

		Parts parts = GameObject.Find("Parts").GetComponent<Parts>();
		WheelFrictionCurve wc_f = new WheelFrictionCurve();
		WheelFrictionCurve wc_s = new WheelFrictionCurve();
		w = new WheelCollider[4];
		w[0]=wheels.FL.GetComponent<WheelCollider>(); w[1]=wheels.FR.GetComponent<WheelCollider>(); 
		w[2]=wheels.RL.GetComponent<WheelCollider>(); w[3]=wheels.RR.GetComponent<WheelCollider>();

		wc_f.extremumSlip=wheel.forward.e_slip; wc_f.extremumValue=wheel.forward.e_value;
		wc_f.asymptoteSlip=wheel.forward.a_slip; wc_f.asymptoteValue=wheel.forward.a_value; wc_f.stiffness=wheel.forward.stiffness;

		wc_s.extremumSlip=wheel.sideways.e_slip; wc_s.extremumValue=wheel.sideways.e_value;
		wc_s.asymptoteSlip=wheel.sideways.a_slip; wc_s.asymptoteValue=wheel.sideways.a_value; wc_s.stiffness=wheel.sideways.stiffness;

		foreach(WheelCollider e in w)
		{
			e.forwardFriction=wc_f; e.sidewaysFriction=wc_s;
		}

		m.maxMotorTorque=body.motor_torque+wheel.buff.torque; 
		m.accel_force = body.accel_force*wheel.buff.accel_force+spoiler.buff.accel_force; 
		m.accel_top = body.accel_top*wheel.buff.accel_top+spoiler.buff.accel_top;
		m.accel_up = body.accel_up;
		m.boost_power = spoiler.boost.power + front.boost.power; 
//		m.boost_amount = spoiler.boost.amount + front.boost.amount; 
		m.boost_max = spoiler.boost.amount + front.boost.amount;
		m.boost_fill_ratio = spoiler.boost.fill_ratio + front.boost.fill_ratio;
		m.boost_consumption = spoiler.boost.consumption + front.boost.consumption;
		m.downforce=body.downforce + wheel.buff.downforce + spoiler.buff.downforce + front.buff.downforce;
		GetComponent<Rigidbody>().mass=body.mass+wheel.buff.mass+spoiler.buff.mass; GetComponent<Rigidbody>().centerOfMass=body.center_mass;
		m.final_steer_force = final_steer_force=body.steer_force+wheel.steer_force;
		m.steer_limit = steer_limit = body.steer_limit+wheel.steer_limit;
		m.drift_boost=wheel.buff.drift_boost+spoiler.buff.drift_boost+front.buff.drift_boost;
		damage.health=max_health=body.health+wheel.health;

#endregion
		Place_Parts ();

	}

	void Update () 
	{
		debug_health=health;
	}

	public void Wreck()
	{
		damage.health=body.health+wheel.health;
		Instantiate(Resources.Load("Explosion"), this.transform.position, new Quaternion(0,0,0,0));
		this.transform.position=race.Last_Checkpoint(pc.ID).position;
		this.transform.rotation=race.Last_Checkpoint(pc.ID).rotation;
		Rigidbody temp = this.GetComponent<Rigidbody>();
		temp.velocity=Vector3.zero; temp.angularVelocity = Vector3.zero;

	}

	public void Place_Parts()
	{
		#region Models

		#endregion

		#region Positions
		wheels.FR.transform.localPosition = body.wheel[1];
		wheels.FL.transform.localPosition = body.wheel[0];
		wheels.RR.transform.localPosition = body.wheel[3];
		wheels.RL.transform.localPosition = body.wheel[2];
		#endregion
	}

	public void Set_Grip(float modifier)
	{
		foreach (WheelCollider e in w) 
		{
			WheelFrictionCurve wc_s = new WheelFrictionCurve();
			wc_s.extremumSlip=wheel.sideways.e_slip; wc_s.extremumValue=wheel.sideways.e_value;
			wc_s.asymptoteSlip=wheel.sideways.a_slip; wc_s.asymptoteValue=wheel.sideways.a_value; wc_s.stiffness=wheel.sideways.stiffness;

			wc_s.stiffness*=modifier;
			e.sidewaysFriction=wc_s;
		}
	}

	public float Slip_Average()
	{
		WheelHit h1, h2, h3, h4;
		w [0].GetGroundHit (out h1); w [1].GetGroundHit (out h2); w [2].GetGroundHit (out h3); w [3].GetGroundHit (out h4);
		return (h1.sidewaysSlip + h2.sidewaysSlip + h3.sidewaysSlip + h4.sidewaysSlip) / 4;
	}

	public float health 
	{
		get
		{return damage.health;}
		set
		{damage.Damage (value);}
	}
}

[System.Serializable]
public class axles
{
	public GameObject FR, FL, RR, RL;
}