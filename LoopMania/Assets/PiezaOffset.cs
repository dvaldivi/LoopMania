using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiezaOffset : MonoBehaviour {
    public Vector3 rotacion;
    public Vector3 posicionRot;
    public Vector3 posicionNext;
    public bool rotate;
    public bool sentido;
    public float time;
    public int nivel;
    // Use this for initialization
    void Start () {
        this.transform.eulerAngles = rotacion;
        this.transform.position += posicionRot;
      
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * 0.2f;
        if (rotate) {
            if(sentido)
                this.transform.Rotate(0, 0, 5  * nivel * Time.deltaTime);
            else
                this.transform.Rotate(0, 0, -5  * nivel*Time.deltaTime);
        }
	}
}
