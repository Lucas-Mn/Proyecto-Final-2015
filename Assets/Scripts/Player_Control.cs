using UnityEngine;
using System.Collections;

public class Player_Control : MonoBehaviour 
{

	public int ID, timer;
	public float smooth_horizontal;
	float end_horizontal;
	Car_Movement move; Main main;

	// Use this for initialization
	void Awake()
	{

	}

	void Start () 
	{
		move = GetComponent<Car_Movement> ();	
		main = GetComponent<Main> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void FixedUpdate()
	{ 		timer++;
			if(timer>1000)
			{
			end_horizontal = Mathf.Lerp(end_horizontal, Input.GetAxisRaw ("Horizontal_"+ID), smooth_horizontal);
			move.input(Input.GetAxisRaw ("Vertical_"+ID), end_horizontal, Input.GetButton("Boost_"+ID), Input.GetAxis("Aerial_X_"+ID), Input.GetAxis("Aerial_Y_"+ID));
			if(Input.GetButton("Fire1_"+ID)) 
				main.gun [0].Input ();
			if(Input.GetButton("Fire2_"+ID)) 
				main.gun [1].Input ();
			if(Input.GetButton("Jump_"+ID)) 
				move.Jump();
			if(Input.GetButtonDown("Reset_"+ID)) main.Wreck();
			}
			
			
		else move.input(0, 0, false, 0, 0);
	}
}
