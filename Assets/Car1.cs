using UnityEngine;
using System.Collections;

public class Car1 : MonoBehaviour {
	public float dx;
	float max_speed;
	int henkou;
	public int type;
	int near;
	int cnt;
	public GameObject Camera;

	int skill;

	// Use this for initialization
	void Start () {
		float r = Random.Range (0.0f, 0.2f);
		r = r + 0.8f;
		dx = r;
		max_speed = r;

		int rr = (int)Random.Range (0,5);
		if (rr == 0) {
			type = 0;
			this.transform.position = new Vector3 (0, 0, 0.0f);
		} else if (rr == 1) {
			type = 1;
			this.transform.position = new Vector3 (0, 0, 5.0f);
		} else {
			type = 2;
			this.transform.position = new Vector3 (0, 0, 10.0f);
		}

		rr = (int)Random.Range (0,5);
		if (rr == 0) {
			skill = 1;
		} else if (rr == 1) {
			skill = 2;
		} else {
			skill = 0;
		}

	}
	
	// Update is called once per frame
	void Update () {

		//すべての車の位置を取得
		float mx = this.transform.position.x;
		GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
		int flag = 0;
		foreach (GameObject obs in cars) {
			float tx = obs.transform.position.x;
			if (tx > mx && tx - mx < 10 && 
				(obs.transform.position.z - this.transform.position.z)*(obs.transform.position.z - this.transform.position.z) < 6.25f) {
				flag = 1;
				near = 1;
				break;
			}
		}
		if (flag == 1) {
			dx = dx - 0.1f;
		} else {
			dx = dx + 0.1f;
		}

		//合流
		flag = 0;
		near = 0;
		if (type == 2) {
			foreach (GameObject obs in cars) {
				Car1 c = obs.GetComponent<Car1> ();
				float tx = obs.transform.position.x;
				if (c.type == 1) {
					if ((mx - tx) * (mx - tx) < 160) {
						flag = 1;
						break;
					}
				}
				if (c.type == 2) {
					if (tx - mx > 0) {
						near = 1;
					}
				}
			}
			//運転技術が高い車は，車線変更できるように減速する
			if(flag == 1 && mx > 100.0f){
				dx = 0.4f;
			}
			//車線変更可能なとき
			//マナーの悪い車は，前の車がいても車線変更をする
			if (flag == 0 && mx > 100.0f && (near == 0 || skill == 2)) {
				type = 1;
				henkou = 1;
				cnt = 10;
			}
			//限界まで来たら停止する
			if (this.transform.position.x > 140) {
				dx = 0.0f;
			}
		}

		//車線変更処理
		if (henkou == 1) {
			this.transform.position -= new Vector3 (0.0f, 0.0f, 0.5f);
			cnt--;
			if (cnt < 0) {
				henkou = 0;
			}
		}

		//移動処理
		if (dx < 0.0f) {
			dx = 0.0f;
		}
		if (dx > max_speed) {
			dx = max_speed;
		}
		this.transform.position += new Vector3(dx, 0.0f, 0.0f);
		
		//もし，範囲外になったら削除
		if (this.transform.position.x > 300) {
			Destroy (gameObject);
		}
			
	}
}
