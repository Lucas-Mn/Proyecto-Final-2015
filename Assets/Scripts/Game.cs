using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	public Camera cam1, cam2, cam3, cam4;
	public uint players, laps;
	public string track;
	Rect r; Rect zero = new Rect(0,0,0,0);
	public GameObject[] tracks;
	public Race race;
	public HUD[] hud = new HUD[4];

	// Use this for initialization
	void Start () 
	{
		switch(players)
		{
		case 1:
			r = new Rect(0,0,1,1);
			cam1.rect=r; cam2.rect=zero; cam3.rect=zero; cam4.rect=zero;
			break;
		case 2:
			r = new Rect(0,0.5f,1,0.5f);
			cam1.rect=r;
			r.y=0; cam2.rect=r;
			cam3.rect=zero; cam4.rect=zero;
			break;
		case 3:
			r = new Rect(0,0.5f,0.5f,0.5f); cam1.rect=r;
			r.x=0.5f; cam2.rect=r; r.x=0.25f; r.y=0; cam3.rect=r;
			cam4.rect=zero;
			break;
		case 4:
			r = new Rect(0,0.5f,0.5f,0.5f); cam1.rect=r;
			r.x=0.5f; cam2.rect=r; r.x=0f; r.y=0; cam3.rect=r;
			r.x=0.5f; cam4.rect=r;
			break;
		}	
		race = GameObject.Find (track).GetComponentInChildren<Race> ();
	}

	void Awake()
	{
		foreach(GameObject e in tracks)
		{
			if(e.name!=track)e.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i<4; i++)
			if (race.laps [i] > laps) 
			{	
				for(int e = 0; e<4; e++)
			    {
				if(e!=i)hud[e].Finish(false);	
				}
				hud [i].Finish (true);
			}
	}
}
