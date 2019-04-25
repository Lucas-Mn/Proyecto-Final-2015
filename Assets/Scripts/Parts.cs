using UnityEngine;
using System.Collections;

public class Parts : MonoBehaviour {

	public Wheel[] wheel;
	public Body[] body;
	public Addon[] spoiler_addon, front_addon;

	void Start () {
	
	}

	void Update () {
	
	}
}

[System.Serializable]
public class Wheel
{
	public string name;
	public bool standard;
	public float steer_force, steer_limit;
	public Vector3 inversion;
	public Passive buff;
	public Tire forward;
	public Tire sideways;
	public int health;
}

[System.Serializable]
public class Body
{
	public string name;
	public float motor_torque, top_speed;
	public float accel_force, accel_top, accel_up;
	public float steer_force, steer_limit;
	public float downforce;
	public float mass;
	public Vector3 center_mass;
	public Vector3 correction;
	public Vector3[] wheel;
	public Vector3 mid_wheel; //for tank wheels, etc
	public Vector3 spoiler_addon, front_addon;
	public int health;
}

[System.Serializable]
public class Addon
{
	public string name, gun_name;
	public Vector3 pos_correction, angle_correction;
	public string type;
	public Passive buff; 
	public Boost boost;
}

[System.Serializable]
public class Passive
{
	public float torque, downforce, drift_boost, accel_force, accel_top, mass;
}

[System.Serializable]
public class Boost
{
	public float power, amount, consumption, fill_ratio;
}

[System.Serializable]
public class Tire
{
	public float e_slip, e_value, a_slip, a_value, stiffness;
}