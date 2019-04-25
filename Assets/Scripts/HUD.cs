using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	public Car_Movement kart; public Car_Damage dam;
	public int timer;
	Race race; public int ID;
	Text text_speed, text_boost, text_timer, text_laps, text_health, text_countdown;

	// Use this for initialization
	void Awake()
	{
		ID=int.Parse(transform.root.gameObject.name.Substring(4));

		text_speed=transform.FindChild("text_speed").GetComponent<Text>();
		text_boost=transform.FindChild("text_boost").GetComponent<Text>();
		text_timer=transform.FindChild("text_timer").GetComponent<Text>();
		text_laps=transform.FindChild("text_laps").GetComponent<Text>();
		text_health=transform.FindChild("text_health").GetComponent<Text>();
		text_countdown=transform.FindChild("text_countdown").GetComponent<Text>();

	}

	void Start()
	{
		race=GameObject.Find(GameObject.Find("Game").GetComponent<Game>().track).GetComponentInChildren<Race>();
	}

	void FixedUpdate()
	{
		timer++;
	}
	
	// Update is called once per frame
	void Update () 
	{
		text_speed.text="Speed: " + Mathf.Round(kart.speed);
		text_boost.text="Boost: " + Mathf.Round(kart.boost_amount);
		text_timer.text="Time: " + kart.debug_timer/10;
		text_laps.text="Laps: " + Mathf.Clamp(race.laps[ID-1], 1, Mathf.Infinity);
		text_health.text="Health: " + kart.main.damage.health.ToString();
		if(timer<500)
			{text_countdown.text="Ready..."; text_countdown.color=Color.red;}
		else if(timer<1000)
			{text_countdown.text="Set..."; text_countdown.color=Color.yellow;}
		else if(timer<1350)
			{text_countdown.text="GO!"; text_countdown.color=Color.green;}
		else if(timer<1500) text_countdown.text="";
	}

	public void Finish(bool win)
	{Debug.Log (ID+win.ToString());
		if (win)
			text_countdown.text = "gud job u win bb ; - )";
		else
			text_countdown.text = "u loooose bb sry trolled";
	}
}
