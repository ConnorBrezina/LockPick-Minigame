using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour {


	private int PickPosition = 0;
	private int PostPos = 0;
	private float Ttime;
	private float Seconds = 60;
	public Text[] texts;
	private float Tension;
	protected int MaxTension;
	protected int MaxgoodTension;
	protected int MedgoodTension;
	protected int MingoodTension;
	private int Wincond;
	public int Difficulty = 3;
	private int DifMultiplier;
	public int LockPickingLevel = 10;
	public GameObject[] moveables;
	private int x = 5;
	public Rigidbody[] rbs;
	private float[] startpos;
	public Button butt;
	// Use this for initialization
	void  Awake(){
		if (Difficulty == 1)
			texts [1].text = "Difficulty EASY";
		if (Difficulty == 2)
			texts [1].text = "Difficulty MEDIUM";
		if (Difficulty == 3)
			texts [1].text = "Difficulty HARD";

		Ttime = 180 - ((Difficulty - 1) * 60);
		startpos = new float[x];
		for (int i = 0; i < x; i++) {
			startpos[i] = moveables[i].transform.position.y;

		}
		for (int i = 0; i < x; i++) {
			rbs [i] = moveables[i].GetComponent<Rigidbody> ();

		}
		DifMultiplier = (((Difficulty * Difficulty)-1) * 10);
	}
		
	void Start () {
		MaxTension = Random.Range ((1500 + (10 * LockPickingLevel) - (DifMultiplier *10)), (2000 + (10 * LockPickingLevel) - (DifMultiplier*10)));
		MedgoodTension = Mathf.RoundToInt((0.8f*(Random.Range(1400f, 1900f))));
		MaxgoodTension = MedgoodTension + (Random.Range((150 + LockPickingLevel- DifMultiplier), (250 + LockPickingLevel- DifMultiplier)));
		MingoodTension = MedgoodTension - (Random.Range((150 + LockPickingLevel- DifMultiplier), (250 + LockPickingLevel- DifMultiplier)));
		Debug.Log ("Good Tension: " + MaxgoodTension + ", " + MingoodTension + " Max Tension: " + MaxTension);
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (Tension + ", " + MingoodTension);
		if (Seconds < 0 && Ttime > 1)
			Seconds = 60;
		Ttime -= Time.deltaTime;
		Seconds -= Time.deltaTime;
		texts [0].text = "Time: " + Mathf.Floor(Ttime /60).ToString("f0") + "." + (Seconds).ToString("f0");
		if (MaxTension < MaxgoodTension) {
			MaxTension = Random.Range ((1500 + (10 * LockPickingLevel) - (DifMultiplier *10)), (2000 + (10 * LockPickingLevel) - (DifMultiplier*10)));
			MedgoodTension = Mathf.RoundToInt((0.8f*(Random.Range(1400f, 1900f))));
			MaxgoodTension = MedgoodTension + (Random.Range((150 + LockPickingLevel- DifMultiplier), (250 + LockPickingLevel- DifMultiplier)));
			MingoodTension = MedgoodTension - (Random.Range((150 + LockPickingLevel- DifMultiplier), (250 + LockPickingLevel- DifMultiplier)));
			Debug.Log ("Good Tension: " + MaxgoodTension + ", " + MingoodTension + " Max Tension: " + MaxTension);
		}
		if (Tension < MingoodTension)
			texts [2].color = Color.white;
		if (Tension > MingoodTension && Tension < MaxgoodTension)
			texts [2].color = Color.green;
		if (Tension > MaxgoodTension)
			texts [2].color = Color.red;


		if (PickPosition > 3) {
			PickPosition = 3;
		}
		if (PickPosition < 0) {
			PickPosition = 0;
		}
		if (Input.GetKeyUp("d")){
			PickPosition += 1;

		}
		if (Input.GetKeyUp("a")){
			PickPosition -= 1;

		}
		if (PickPosition != PostPos) {
			if (PickPosition == 0)
				moveables [0].transform.position = new Vector3 (-7.14f, startpos [0], moveables [0].transform.position.z);
			if (PickPosition == 1)
				moveables [0].transform.position = new Vector3 (-4.01f, startpos [0], moveables [0].transform.position.z);
			if (PickPosition == 2)
				moveables [0].transform.position = new Vector3 (-0.87f, startpos [0], moveables [0].transform.position.z);
			if (PickPosition == 3)
				moveables [0].transform.position = new Vector3 (2.26f, startpos [0], moveables [0].transform.position.z);
			PostPos = PickPosition;
		}

		if (Input.GetKey("w")){
			rbs [0].AddForce (Vector3.up *2f, ForceMode.Impulse);
		}
		if (moveables [0].transform.position.y <= startpos [0]) {
			rbs [0].useGravity = false;
			rbs [0].velocity = new Vector3 (0,0,0);
		}
		else
			rbs [0].useGravity = true;
		
		if (moveables [1].transform.position.y <= startpos [1]) {
			rbs [1].useGravity = false;
			rbs [1].velocity = new Vector3 (0,0,0);
		}
		else
			rbs [1].useGravity = true;
		
		if (moveables [2].transform.position.y <= startpos [2]) {
			rbs [2].useGravity = false;
			rbs [2].velocity = new Vector3 (0,0,0);
		}
		else
			rbs [2].useGravity = true;
		
		if (moveables [3].transform.position.y <= startpos [3]) {
			rbs [3].useGravity = false;
			rbs [3].velocity = new Vector3 (0,0,0);
		}
		else
			rbs [3].useGravity = true;
		
		if (moveables [4].transform.position.y <= startpos [4]) {
			rbs [4].useGravity = false;
			rbs [4].velocity = new Vector3 (0,0,0);
		}
		else
			rbs [4].useGravity = true;

		if (Tension > MaxTension || Ttime < 0) 
		{
			Debug.Log ("You lose");
		}
		if (Wincond >= 1)
			Debug.Log ("You Win");
		if (Tension < MingoodTension)
			Wincond = 0;
		

		if (Tension > MingoodTension && Tension < MaxgoodTension && moveables [1].transform.position.y > 0.04 && moveables [1].transform.position.y < 0.15 && Tension > MingoodTension && Tension < MaxgoodTension && moveables [2].transform.position.y > 0.08 && moveables [2].transform.position.y < 0.15 && Tension > MingoodTension && Tension < MaxgoodTension && moveables [3].transform.position.y > -0.02 && moveables [3].transform.position.y < 0.07  && Tension > MingoodTension && Tension < MaxgoodTension && moveables [4].transform.position.y > 0.02 && moveables [4].transform.position.y < 0.12 ) {
				Wincond = 1;
			
				Debug.Log ("WinCond++");
			}

		if (Tension > MingoodTension && Tension < MaxgoodTension && moveables [1].transform.position.y > 0.04 && moveables [1].transform.position.y < 0.15) {
			rbs [1].useGravity = false;
			rbs [1].velocity = new Vector3 (0, 0, 0);
		}
		else if (moveables [1].transform.position.y > startpos [1]) {
			rbs [1].useGravity = true;
		}
		if (Tension > MingoodTension && Tension < MaxgoodTension && moveables [2].transform.position.y > 0.08 && moveables [2].transform.position.y < 0.15) {
			rbs [2].useGravity = false;
			rbs [2].velocity = new Vector3 (0, 0, 0);
		}
		else if (moveables [2].transform.position.y > startpos [2]) {
			rbs [2].useGravity = true;
		}
		if (Tension > MingoodTension && Tension < MaxgoodTension && moveables [3].transform.position.y > -0.02 && moveables [3].transform.position.y < 0.07) {
			rbs [3].useGravity = false;
			rbs [3].velocity = new Vector3 (0, 0, 0);
		}
		else if (moveables [3].transform.position.y > startpos [3]) {
			rbs [3].useGravity = true;
		}
		if (Tension > MingoodTension && Tension < MaxgoodTension && moveables [4].transform.position.y > 0.02 && moveables [4].transform.position.y < 0.12) {
			rbs [4].useGravity = false;
			rbs [4].velocity = new Vector3 (0, 0, 0);
		}
		else if (moveables [4].transform.position.y > startpos [4]) {
			rbs [4].useGravity = true;
		}
			
			
	}
	void FixedUpdate()
	{
		if(Tension > 0){
			Tension -= (1f + Tension/750 + (Difficulty -1)  - LockPickingLevel /10);
		}
	}
	public void TensionUp()
	{
		Tension += 100 + (50 * (Difficulty -1));
		butt.transform.position = new Vector3 (253f + 317.5f, Random.Range(-60f, 60f)+145.5f, 0f);

	}
}
