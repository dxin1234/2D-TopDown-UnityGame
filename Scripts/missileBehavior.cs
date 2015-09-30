using UnityEngine;
using System.Collections;

public class missileBehavior : MonoBehaviour {

	public float speed;
	Transform myTrans;
	Vector3 myPos;
	public Vector3 mouseposition;
	float angle;

	// Use this for initialization
	void Start () {
		myTrans = transform;
		myPos = transform.position;
		float mousex = (Input.mousePosition.x);
		float mousey = (Input.mousePosition.y);
		mouseposition = Camera.main.ScreenToWorldPoint(new Vector3 (mousex,mousey,0));

		Vector3 moveDirection = mouseposition - transform.position; 
		if (moveDirection != Vector3.zero) 
		{
			angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
	
	// Update is called once per frame
	void Update () {
		myPos.x += (Mathf.Cos (angle) * speed) * Time.deltaTime;
		myPos.y += (Mathf.Sin (angle) * speed) * Time.deltaTime;
		myTrans.position = myPos;
	}
}
