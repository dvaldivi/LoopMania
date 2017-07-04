using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour {
    public GameObject lastTrack;
    public GameObject[] pistas;
    public float time;
    private bool loop;
	// Use this for initialization
	void Start () {

        lastTrack=  Instantiate(pistas[1].gameObject, new Vector3(-8,0,0), this.transform.rotation);
        lastTrack.transform.parent = this.transform;
        
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(player.transform.position, lastTrack.transform.position) < 20) {
            if (time < 0)
            {

                generaPistaRandom();
                time = 0.5f;
                loop = false;
            }
            
        };
        if (player.transform.position.y < -10) {
            this.Reset();
        }
        
    }

    public void generaPista() {

    }
    public void Reset()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        this.GetComponent<GameController>().points = 0;
        lastTrack = Instantiate(pistas[1].gameObject, new Vector3(-8,0,0), this.transform.rotation);
        lastTrack.transform.parent = this.transform;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Car>().time = 0;
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-3, 3, 0);
        GameObject.FindGameObjectWithTag("Player").transform.eulerAngles = new Vector3(0, 90, 0);
    } 
    public void generaPistaRandom()
    {

        // Random.Range(0,2)
        lastTrack =  Instantiate(pistas[1].gameObject, lastTrack.transform.position + lastTrack.GetComponent<PiezaOffset>().posicionNext, this.transform.rotation);
        lastTrack.transform.parent = this.transform;
        int random = (int)Random.Range(0, 2);
        if (random == 0)
        {
            random = (int)Random.Range(0, 2);
            if (random == 0)
            {
                random = (int)Random.Range(1, 5);
                lastTrack.GetComponent<PiezaOffset>().nivel = random;
                lastTrack.GetComponent<PiezaOffset>().rotate = true;
                lastTrack.GetComponent<PiezaOffset>().sentido = true;
            }
            else {
                lastTrack.GetComponent<PiezaOffset>().rotate = true;
                lastTrack.GetComponent<PiezaOffset>().sentido = false;
            }
        }
        else {

            lastTrack.GetComponent<PiezaOffset>().rotate = false;
        }

       
    }
}
