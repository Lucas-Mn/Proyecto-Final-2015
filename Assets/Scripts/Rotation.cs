using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
            if (Input.GetKey(KeyCode.RightArrow))
                transform.Rotate(Vector3.up, 30.0f * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftArrow))
                transform.Rotate(Vector3.up, -30.0f * Time.deltaTime);
    }
}
