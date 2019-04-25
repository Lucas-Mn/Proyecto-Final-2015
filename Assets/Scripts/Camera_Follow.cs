using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {

	public Transform target;
	public float smoothTime = 2F;
	public float plus_y;
	public bool lock_pos_y, lock_rot_x, lock_rot_z;
	public Vector3 pos;
	private Vector3 velocity = Vector3.zero;
	Car_Movement m; Transform w;
	void Awake()
	{
		m=target.gameObject.GetComponent<Car_Movement>();
		w=target.FindChild("wheels").FindChild("RL");
	}
	void Update() 
	{

//		Vector3 targetPosition = target.TransformPoint(new Vector3(0, 2.5f	, -4));
//		Quaternion targetRotation = target.transform.rotation;
//		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
//		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f);

//		if (transform.root.gameObject.GetComponent<Car_Movement> ().speed > 1) 
//		{
//			Vector3 rotation;
//			rotation = Quaternion.LookRotation (transform.root.gameObject.GetComponent<Rigidbody> ().velocity).eulerAngles;
//			transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.x, rotation.y, transform.rotation.z));
//		}
	}

	void FixedUpdate()
	{
		Quaternion newRotation = transform.rotation;
		Vector3 targetPosition = target.TransformPoint(pos);
		Quaternion targetRotation = target.transform.rotation;
		if(lock_pos_y)targetPosition.y=w.position.y+plus_y;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
//		if(target.gameObject.GetComponent<Car_Movement>().grounded())
		newRotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f);
		newRotation=Quaternion.Euler(lock_rot_x ? 0 : newRotation.eulerAngles.x ,newRotation.eulerAngles.y, lock_rot_z ? 0 : newRotation.eulerAngles.z);
		transform.rotation=newRotation;
	}
}