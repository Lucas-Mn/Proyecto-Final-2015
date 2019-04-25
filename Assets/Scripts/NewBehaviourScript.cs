using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour {
	public List<AxleInfo> axleInfos; // the information about each individual axle
	public float maxMotorTorque; // maximum torque the motor can apply to wheel
	public float maxSteeringAngle; // maximum steer angle the wheel can have
	
	public void FixedUpdate()
	{

	}

	public void axis(float vertical, float horizontal)
	{
		float motor = maxMotorTorque * vertical;
		float steering = maxSteeringAngle * horizontal;
		
		foreach (AxleInfo axleInfo in axleInfos) 
		{
			if (axleInfo.steering) 
			{
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) 
			{	
				if(Input.GetKey(KeyCode.Space))
				{axleInfo.leftWheel.motorTorque=-maxMotorTorque; axleInfo.rightWheel.motorTorque=-maxMotorTorque;}
				else{
					axleInfo.leftWheel.motorTorque = motor;
					axleInfo.rightWheel.motorTorque = motor;}
			}
			
			axleInfo.leftWheel.brakeTorque=Input.GetAxis("Vertical")<0 ? 1000 * -Input.GetAxis("Vertical") : 0;
			axleInfo.rightWheel.brakeTorque=Input.GetAxis("Vertical")<0 ? 1000 * -Input.GetAxis("Vertical") : 0;
			
		}
	}
}

