using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Car_Movement : MonoBehaviour {

	public List<AxleInfo> axleInfos; // the information about each individual axle
	public uint debug_timer;
	public bool ground, boosting;
	public float debug_motor;
	public float maxMotorTorque; // maximum torque the motor can apply to wheel
	public float accel_force, accel_top, accel_final, accel_up;
	public float maxSteeringAngle, out_horizontal; // maximum steer angle the wheel can have
	public float brake_torque;
	public float steer_force, final_steer_force, steer_limit; 
	public float aerial_torque, downforce, final_downforce;
	public float boost_power, boost_amount, boost_consumption, boost_fill_ratio, boost_max;
	public float drift_boost;
	public float speed, slip, real_slip;
	public float jump_force;
	public Vector3 center_mass_modifier;
	public Vector3 final_mass;
	public bool jump_enabled;
	public Rigidbody rigid;
	public Main main;

	public void Start()
	{
		main = GetComponent<Main> ();
		rigid = GetComponent<Rigidbody>();
	}

	public void Update()
	{
		speed = rigid.transform.InverseTransformDirection (rigid.velocity).z;
		real_slip = Mathf.Abs( main.Slip_Average () );
		if (speed > 15 && Mathf.Abs(steer_force) > .7)
			slip = Mathf.Abs (main.Slip_Average ());
		else
			slip = 0;
		steer_force = rigid.angularVelocity.y;
		ground = grounded();
	}

	public void input(float vertical, float horizontal, bool boost, float aerial_x, float aerial_y)
	{	debug_timer++;
		out_horizontal=horizontal; //for other scripts to access

		#region Torques
		if(grounded())
		{
			if(speed<0)horizontal*=-1;
		
		
		float motor = maxMotorTorque* vertical;
		float steering = maxSteeringAngle * horizontal; //for wheelcollider steering (unused)

		#region Axles
		foreach (AxleInfo axleInfo in axleInfos) 
		{
			if (axleInfo.steering) //wheel steering should not be enabled
			{
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) 
			{	
				if(vertical>=0)
				{
					axleInfo.leftWheel.motorTorque = motor;
					axleInfo.rightWheel.motorTorque = motor;
				}
				if(speed<=0&&vertical<=0)
				{	
					axleInfo.leftWheel.motorTorque = motor;
					axleInfo.rightWheel.motorTorque = motor;
				}
			}
			
			if(speed>0)
			{
				axleInfo.leftWheel.brakeTorque=vertical<0 ? brake_torque * -vertical : 0;
				axleInfo.rightWheel.brakeTorque=vertical<0 ? brake_torque * -vertical : 0;
			}
			else if (speed<0&&vertical>0)
			{
				axleInfo.leftWheel.brakeTorque=brake_torque * vertical;
				axleInfo.rightWheel.brakeTorque=brake_torque * vertical;
			}
			else 
			{
				axleInfo.leftWheel.brakeTorque=0;
				axleInfo.rightWheel.brakeTorque=0;
			}
			
		}
		#endregion

		#region Boost
		if (boost && boost_amount>0)

			{
				boosting=true;
				boost_amount-=boost_consumption;
				rigid.AddRelativeForce (Vector3.forward * (boost_power));
				rigid.AddTorque (transform.right * -500);//makes the car wheelie a bit when boosting, but loses FW grip
			}
		else boosting=false;
		if(boost_amount<boost_max)boost_amount+=slip*speed/30*boost_fill_ratio;
		
		if(boost_max<boost_amount)boost_amount=boost_max;

			rigid.AddRelativeForce (Vector3.forward * drift_boost * slip);
		
		#endregion
		final_downforce = speed/40 * downforce;
		}

		else
		{
			rigid.AddTorque(transform.right*aerial_y*-aerial_torque, ForceMode.Acceleration);
			rigid.AddTorque(transform.forward*aerial_x*-aerial_torque, ForceMode.Acceleration); 
			//if(boost_amount<boost_max)boost_amount+=boost_fill_ratio;
		}
		//steering
		if(steer_force<steer_limit&&steer_force>-main.steer_limit)
		rigid.AddTorque(transform.up*final_steer_force*horizontal*(speed>1?30/speed:1), ForceMode.Acceleration);
		#endregion



		#region Physics
//		if(!grounded())
//		{
//			center_mass_modifier = Vector3.Lerp(center_mass_modifier, new Vector3(0, -0.2f, -rigid.centerOfMass.z), 0.1f);
//		}
//		else
//			center_mass_modifier = Vector3.zero;
		//center of mass
		rigid.centerOfMass=main.body.center_mass+center_mass_modifier;
		final_mass=rigid.centerOfMass;
		//downforce
		rigid.AddForce(transform.up*-downforce);
		//acceleration compensation
		accel_final = accel_force*(accel_top/speed) + accel_up*speed;
		if(vertical>0&&speed>.1f)
		rigid.AddForce(transform.forward*accel_final);
		#endregion

		#region Visuals


		#endregion

	}

	public void Jump()
	{
		if(grounded ()&&jump_enabled)
		rigid.AddForce(transform.up*jump_force, ForceMode.Acceleration);
	}

	public bool grounded()
	{
		foreach (AxleInfo e  in axleInfos) 
		{
			if(!(e.leftWheel.isGrounded && e.rightWheel.isGrounded))
			{
				return false;
			}
		}
		return true;
	}
}

[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor; // is this wheel attached to motor?
	public bool steering; // does this wheel apply steer angle?
}