using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Car : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public GameObject peso;
    private bool aire;
    private bool paso1;
    public float time;
    public float jump_time;


    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;

        visualWheel.transform.rotation = Quaternion.Euler( 0,0, rotation.eulerAngles.x);
    }
    public void FixedUpdate()
    {
        /*jump*/
        if (jump_time < 0) {
            if (Input.GetKey(KeyCode.Space)) {
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 400 * this.GetComponent<Rigidbody>().mass, 0));
                jump_time = 3;
                Debug.Log("salta");
            }
            

        }
        jump_time -= Time.deltaTime;
        /*rotacion 
         * 
         * 
         * 
         * /
         * 
         * 
         * //*/
        if (time < 0.2f)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        time += Time.deltaTime;

        float motor = 0;
        motor = maxMotorTorque * Input.GetAxis("Vertical");

        Vector3 forward = new Vector3(0,-1,0) *1;


        Vector3 wwa = this.transform.forward;
        //Debug.Log(wwa.x);
        float rotacion = wwa.x;

        Debug.DrawRay(transform.position + new  Vector3(0,-0.1f,0), forward, Color.green);
        if (Physics.Raycast(transform.position + new Vector3(0, -0.1f, 0 ), forward, 2))
        {
            aire = true;

        }
        else
        {
            aire = false;
         
        }
    
        if (aire) {
      
            if (paso1 == false) {

                if (wwa.x <  0)
                {
                    paso1 = true;
                    Debug.Log("paso1");

                }

            } /*else if (wwa.x > 0f) {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Loop();
                paso1 = false;
                Debug.Log("LOOOOOOOPPPP");
            }*/


        } 

        /*
         * 
         * contro coche */

        if (Mathf.Abs(this.GetComponent<Rigidbody>().angularVelocity.magnitude) < 25)
            this.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, Input.GetAxis("Horizontal") * 1000 * 0.4f));
       
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        Time.timeScale = 3F;
        if (Input.GetAxis("Vertical") < 0)
        {
            this.GetComponent<Rigidbody>().drag = 0.2f;
        }
        else if (Input.GetAxis("Vertical") > 0.2f)
        {

            this.GetComponent<Rigidbody>().drag = 0.1f;
        }
        
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
                
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
        
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}