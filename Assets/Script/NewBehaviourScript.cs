using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
	private float time = 60;
	public GameObject GameOver;
	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = ((int)time).ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time < 0) 
			time = 0;
			GetComponent<Text> ().text = ((int)time).ToString ();
		if (time == 0) {
			Application.LoadLevel ("Title");
		}
	}
}
