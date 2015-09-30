using UnityEngine;
using System.Collections;

public class trackPlayer : MonoBehaviour {

	Transform player;

	// Use this for initialization
	void Start () {
		GameObject player_go = GameObject.FindGameObjectWithTag("player");
		if (player_go == null) {
			Debug.LogError("Cannot find player");
			return;
		}
		player = player_go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3(player.position.x, player.position.y, transform.position.z);;
		transform.position = pos;
	}
}
