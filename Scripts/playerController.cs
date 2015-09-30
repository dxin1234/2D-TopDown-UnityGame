using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float speed ;
	public float speedVertical;
	Transform myTrans;
	Vector3 myPos;
	Animator animator;

	// variables for walking
	int walkHorizontal = 0;
	int walkVertical = 0;
	bool doWalk = false;
	bool facingRight = false;
	
	public GameObject summon;
	public GameObject missile;

	string state = "default";
	float time;

	public bool spotted = false;

	// Use this for initialization
	void Start () {
		myTrans = transform;
		myPos = myTrans.position;
		animator = myTrans.GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (state == "default") {
			Move ();
			if (Input.GetKey (KeyCode.Space)) {
				// summoning casting time
				time = Time.time;
				state = "summon";
			}
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				time = Time.time;
				state = "punch";
				if (facingRight)
					animator.Play ("guyPunchRight");
				else
					animator.Play ("guyPunch");
			}
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				time = Time.time;
				state = "fire";
			}

		}


	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		
		if (state == "default") {
			Walking ();
		}
		else if (state == "summon") {
			if (facingRight)
				animator.Play ("guySummoningRight");
			else
				animator.Play ("guySummoning");

			if (Time.time - time > 0.75f) {
				Summon ();
				state = "default";
				if (facingRight)
					animator.Play("guyidleRight");
				else 
					animator.Play ("guyidle");
			}

		}
		else if (state == "punch") {

			if (Time.time - time > 0.25f) {
				Punch ();
				state = "default";
				if (facingRight)
					animator.Play("guyidleRight");
				else 
					animator.Play ("guyidle");
			}
		}
		else if (state == "fire") {
			if (Time.time - time > 0.25f) {
				Fire();
				state = "default";
			}
		}
		
		
	}

	void Fire() {
		GameObject dude = Instantiate(missile) as GameObject;
		dude.transform.position = new Vector3(transform.position.x,
		                                      transform.position.y,
		                                      transform.position.y);
	}

	void Punch() {
		RaycastHit2D detectedObject;
		Vector3 checkPos;
		if (facingRight)
			checkPos = new Vector3(myPos.x + 0.75f, myPos.y, myPos.z);
		else
			checkPos = new Vector3(myPos.x - 0.75f, myPos.y, myPos.z);
		//Debug.DrawLine (transform.position, checkPos, Color.green);
		spotted = Physics2D.Linecast(transform.position, checkPos, 1 << LayerMask.NameToLayer("summons"));
		if (spotted) {
			detectedObject = Physics2D.Linecast(transform.position, checkPos, 1 << LayerMask.NameToLayer("summons"));
			
			if(detectedObject.collider.gameObject.tag == "summon")
				detectedObject.collider.gameObject.SendMessage("GetHit", 10);
			
		}
	}
	
	// controls for walking
	void Move() {
		if (Input.GetKey(KeyCode.D)) {
			walkHorizontal = 1;
			doWalk = true;
			facingRight = true;
		}
		if (Input.GetKey(KeyCode.A)) {
			walkHorizontal = 2;
			doWalk = true;
			facingRight = false;
		}
		if (Input.GetKey(KeyCode.W)) {
			walkVertical = 1;
			doWalk = true;
		}
		if (Input.GetKey(KeyCode.S)) {
			walkVertical = 2;
			doWalk = true;
		}
	}

	// walking action and animation
	void Walking() {
		if (doWalk) {
			//rotate object Right & Left
			if (walkHorizontal == 1) {
				myPos.x += speed * Time.deltaTime;
				animator.SetTrigger("doWalkRight");
				walkHorizontal = 0;
			}
			if (walkHorizontal == 2) {
				myPos.x += -speed * Time.deltaTime;
				animator.SetTrigger("doWalk");
				walkHorizontal = 0;
			}
			
			//move object Forward & Backward
			if (walkVertical == 1) {
				myPos.y += speedVertical * Time.deltaTime;
				if (facingRight)
					animator.SetTrigger("doWalkRight");
				else
					animator.SetTrigger("doWalk");
				walkVertical = 0;
			}
			if (walkVertical == 2) {
				myPos.y += -speedVertical * Time.deltaTime; 
				if (facingRight)
					animator.SetTrigger("doWalkRight");
				else
					animator.SetTrigger("doWalk");
				walkVertical = 0;
			}
			doWalk = false;
		}
		else
			animator.SetTrigger("doStop");
		myPos.z = myPos.y - 0.01f;
		myTrans.position = myPos;


	}



	void Summon() {

		GameObject dude = Instantiate(summon) as GameObject;
		if (facingRight)
			dude.transform.position = new Vector3(transform.position.x + 2f,
			                                      transform.position.y,
			                                      transform.position.y);
		else
			dude.transform.position = new Vector3(transform.position.x - 2f,
			                                      transform.position.y,
			                                      transform.position.y);


	}


}








