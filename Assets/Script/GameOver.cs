using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Text>().enabled = false;
	}
	public void Lose(){
		this.gameObject.GetComponent<Text> ().enabled = true;
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("Title");
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
