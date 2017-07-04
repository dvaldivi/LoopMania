using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour {
    private GameObject player;
    public Vector3 desplazamiento;
	// Use this for initialization
	private void buscaPlayer() {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            buscaPlayer();
        }
        else {
            this.transform.position = player.transform.position + desplazamiento;
            float velocidad = player.GetComponent<Rigidbody>().velocity.magnitude;

            if (velocidad > 15)
                this.GetComponent<Camera>().orthographicSize = velocidad / 15 * 15;
            else
                this.GetComponent<Camera>().orthographicSize = 15;


        }
	}
}
