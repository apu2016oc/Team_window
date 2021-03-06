﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shot : MonoBehaviour {
	
	public GameObject bulletPrefab; //弾のプレハブ
	public float power = 500.0f; //弾の威力
	public float speed = 0.1f; //カメラアングルの移動速度
	public int shotCount;
	public Text shellLabel;
	public GameObject GameOver;
	public AudioClip shotSE;
	public AudioClip shotSE1;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		shellLabel.text = "残り;" + shotCount;
	}
	
	// Update is called once per frame
	void Update () {
		//マウスカーソルの位置を取得
		/*Vector3 mousePos = Input.mousePosition;

		//中央の位置に合わせる(マウス座標の開始位置は画面左下なので調整が必要)
		mousePos.x = mousePos.x - ( Screen.width / 2 );
		mousePos.y = mousePos.y - ( Screen.height / 2 );

		//カメラのアングルを取得
		Vector3 cameraAngle = transform.eulerAngles;

		cameraAngle.x = -mousePos.y * speed; //マウスを横に動かしたとき
		cameraAngle.y = mousePos.x * speed; //マウスを縦に動かしたとき
		cameraAngle.z = 0; //ｚは変化しないので0にする

		//動かした変化をカメラのアングルに適用させる
		transform.eulerAngles = cameraAngle;
        */
		//左クリック
		if (Input.GetMouseButtonDown (0)) {
			if (shotCount > 0) {
				//弾を生成
				GameObject bullet = Instantiate (bulletPrefab, this.transform.position, transform.rotation) as GameObject;
				//弾が前方に飛ぶように力を加える
				bullet.GetComponent<Rigidbody> ().AddForce (transform.forward * power);
				GetComponent<AudioSource>().PlayOneShot (shotSE);
				shotCount -= 1;
				shellLabel.text = "残り;" + shotCount;
			} else if (shotCount == 0) {
				GameOver.SendMessage ("Lose");
				GetComponent<AudioSource>().PlayOneShot (shotSE1);
				}
			}
		if (Input.GetKeyDown ("space")) {
			Application.LoadLevel ("Title");
		}
	}
}
