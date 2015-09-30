using UnityEngine;
using System.Collections;

public class summonBehavior : MonoBehaviour {

	public bool collisionDetected = false;
	public int life = 30;
	float time;
	Animator animator;
	public float speed;
	public float speedVertical;
	bool facingRight = false;

	Transform myTrans;
	Vector3 myPos;

	public GameObject player;
	string state = "default";

	public int num;


	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
		player = GameObject.Find("_playerDude");
		myTrans = transform;
		myPos = myTrans.position;
		time = Time.time;
		num = Random.Range (0,8);
	}
	
	// Update is called once per frame
	void Update () {
		if (life == 0)
			Destroy(this.gameObject);

		switch(state) {
		case "default":
			follow ();
			float randNum = Random.Range (0.5f, 2f);
			if (Time.time - time > randNum) {
				num = Random.Range(0,8);
				time = Time.time;
			}
			break;
		case "gotHit":
			if (Time.time - time > 0.15) {
				animator.Play("summon");
				state = "default";
			}
			break;
		default:
			break;

		}

	}

	void follow() {

		if (num == 0) {
			myPos.x += speed * Time.deltaTime;
			animator.SetTrigger("doWalkRight");
			facingRight = true;
		}
		if (num == 1) {
			myPos.x += -speed * Time.deltaTime;
			animator.SetTrigger("doWalk");
			facingRight = false;
		}
		
		//move object Forward & Backward
		if (num == 2) {
			myPos.y += speedVertical * Time.deltaTime;
			if (facingRight)
				animator.SetTrigger("doWalkRight");
			else
				animator.SetTrigger("doWalk");
		}
		if (num == 3) {
			myPos.y += -speedVertical * Time.deltaTime; 
			if (facingRight)
				animator.SetTrigger("doWalkRight");
			else
				animator.SetTrigger("doWalk");
		}
		if (num == 4 || num == 5 || num == 6 || num == 7) {
			animator.SetTrigger ("doStop");
		}
		myPos.z = myPos.y;
		transform.position = myPos;

	}


	void fixedUpdate() {

	}

	void GetHit(int damage) {
		time = Time.time;
		life = life - damage;
		state = "gotHit";
		animator.Play("summonGetHit");
	}
}
