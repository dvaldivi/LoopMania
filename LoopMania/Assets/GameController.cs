using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private GameObject Player;
    public float points;
    public float time;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        points = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(0, 0, 0)) ; 
        time = time - Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R)) {
            this.GetComponent<TrackGenerator>().Reset();
        }
	}
    public void Loop() {

        points += 10;
        time = 3;
    }
    void OnGUI()
    {
        GUILayout.Label("Points " + ((int)points).ToString());
        if(time > 0 )
        GUI.Label(new Rect(Screen.width - Screen.width/2 -20 , 0, 100, 20), "Loop");
   
    }
}
