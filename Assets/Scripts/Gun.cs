using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public gun_type gun; 
	public string class_name;

	void Awake()
	{

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		gun.Update();
	}

	public void Set(string s)
	{
		switch (s) 
		{
		case "disabled": gun=new Disabled(); break;
		case "minigun": gun=new Minigun(); break;
		case "saw": gun=new Saw_Blade(); break;
		case "mine": gun=new Mine_Layer(); break;
		case "cannon": gun=new Cannon(); break;
		}

		gun.transform=this.transform;
		gun.Start();
		class_name=s;


	}

	public void Input()
	{
		gun.input();
	}
}

[System.Serializable]
public class gun_type
{
	public string name;
	public Transform transform;
	public virtual void Start(){}
	public virtual void Update(){}
	public virtual void input(){}
	public virtual void box(){}//after picking up a box
	public virtual string out_1{ get; set;}
	public virtual string out_2{ get; set;}
}

public class Disabled : gun_type
{

}

[System.Serializable]
public class Minigun : gun_type
{
	int ammo, max_ammo, damage;
	float range, cooldown, max_cooldown, inaccuracy;
	Transform barrel, end;
	ParticleSystem particles;
	bool shot;

	public Minigun()
	{
		name="minigun";
		damage = 30; max_cooldown = .05f;
		range = 25.0f;
		inaccuracy = .1f;
		shot = false;
	}

	public override void Start()
	{
		barrel=transform.FindChild("body").FindChild("front").FindChild("barrel");
		end = barrel.FindChild("Tapa");
		particles = transform.FindChild("body").FindChild("front").FindChild("Particle").GetComponent<ParticleSystem>();
		particles.emissionRate=0;
	}

	public override void Update()
	{
		cooldown-=Time.deltaTime;
		if(shot)particles.emissionRate=10;
		else particles.emissionRate=0;
		shot=false;
	}

	public override void input()
	{
		shot=true;
		//move barrel
		barrel.Rotate(0,0,2.25f);
		if(cooldown<0)
		{
		cooldown = max_cooldown;
		//set inaccuracy
			float ax = (1 - 2 * Random.value )* inaccuracy;
			float ay = (1 - 2 * Random.value )* inaccuracy;
			float az = 1;
		RaycastHit hit; 
		if (Physics.Raycast (end.position, transform.TransformDirection(new Vector3(ax, ay, az)), out hit))
		{
			if(hit.collider.transform.root.gameObject.tag=="kart")
			hit.collider.transform.root.gameObject.GetComponent<Car_Damage>().Damage(damage);
			else if(hit.collider.gameObject.tag=="ball") GameObject.Destroy(hit.collider.gameObject);
		}
			Debug.DrawRay(end.position, transform.TransformDirection(new Vector3(ax, ay, az*10)));
		}
	}
	public override void box()
	{
		ammo += 30; if (ammo > max_ammo)ammo = max_ammo;
	}
	public override string out_1
	{
		get{return ammo.ToString();}
	}
	public override string out_2
	{
		get{return max_ammo.ToString();}
	}
}

[System.Serializable]
public class Saw_Blade : gun_type
{
	Car_Movement m;
	Transform blade;

	public Saw_Blade()
	{
		name = "saw blade";
	}

	public override void Start()
	{
		blade=transform.FindChild("body").FindChild("front").FindChild("Scythe");
		m = transform.GetComponentInParent<Car_Movement>();
	}

	public override void Update()
	{
		blade.Rotate(new Vector3(0,0,10f*Time.deltaTime*m.speed));
	}

	public override void input()
	{

	}

}

[System.Serializable]
public class Mine_Layer : gun_type
{
	float cooldown, max_cooldown;
	Transform t;

	public Mine_Layer()
	{
		max_cooldown=10;
	}

	public override void Start()
	{
		t=transform.FindChild("body").FindChild("spoiler");
	}

	public override void Update()
	{
		cooldown-=Time.deltaTime;
	}

	public override void input()
	{
		if(cooldown<0)
		{
			cooldown=max_cooldown;
		GameObject.Instantiate(Resources.Load("Mine"), t.position, t.rotation);
		}
	}

}

[System.Serializable]
public class Cannon : gun_type
{
	float cooldown, max_cooldown;
	Transform t;

	public Cannon()
	{
		max_cooldown=1;
	}

	public override void Start()
	{
		t=transform.FindChild("body").FindChild("front").FindChild("Pos");
	}
	
	public override void Update()
	{
		cooldown-=Time.deltaTime;
	}
	
	public override void input()
	{
		if(cooldown<0)
		{
			cooldown=max_cooldown;
			GameObject ball = GameObject.Instantiate(Resources.Load("Ball"), t.position, transform.rotation) as GameObject;
			ball.GetComponent<Rigidbody>().AddForce(t.forward*1000 + t.forward*transform.GetComponent<Car_Movement>().speed*100, ForceMode.Force);
		}
	}
}




