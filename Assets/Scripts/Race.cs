using UnityEngine;
using System.Collections;

public class Race : MonoBehaviour {

	public Checkpoint[] points;
	public int[] laps;

	void Start () 
	{
		Set();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Check(int ID, int n)
	{
		if(n!=0)
		{
			if(points[n-1].has_id[ID])
			{
				points[n].has_id[ID]=true;
				points[n-1].has_id[ID]=false;
			}
		}
		else
		{
			if(points[points.Length-1].has_id[ID])
			{
				laps[ID]++;
				points[n].has_id[ID]=true;
				points[points.Length-1].has_id[ID]=false;
			}
		}
	}

	public void Set()
	{
		points =  GetComponentsInChildren<Checkpoint>();
		for(int i=0;i<points.Length;i++)
		{
			points[i].number=i;//sets the checkpoint's order number
			points[i].has_id=new bool[GameObject.FindGameObjectsWithTag("kart").Length];//wether each player passed the checkpoint
			for(int i2=0;i2<points[points.Length-1].has_id.Length;i2++)//set last point to false
				points[points.Length-1].has_id[i2]=true;//so that start can be checked
		}
		laps=new int[points[0].has_id.Length];//set size to number of racers
	}

	public Transform Last_Checkpoint(int id)
	{
		foreach(Checkpoint e in points)
		{
			if(e.has_id[id-1]) return e.transform;
		}
		return points[0].transform;
	}

	class racer
	{
		public int ID;
	}
}
