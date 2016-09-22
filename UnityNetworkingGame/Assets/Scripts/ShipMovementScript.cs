using UnityEngine;
using System.Collections;

public class ShipMovementScript : MonoBehaviour {

    public float moveSpdMax;
    private float moveSpeedMult;

    private Vector3 momentum;

    public float turnRateMax;
    private float turnRateVal;

    public Vector3 angMomentum;

    public float momentumMag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        momentumMag = Vector3.Magnitude(momentum);

        //if (Input.GetAxis("Mouse ScrollWheel") > 0)
        //{
        //    moveSpeedMult += .1f;
        //}
        //if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //{
        //    moveSpeedMult -= .1f;
        //}
        //moveSpeedMult = Mathf.Clamp(moveSpeedMult, 0f, 1f);

        if (Input.GetKey(KeyCode.W))
        {
            moveSpeedMult = 1;
        }
        else
        {
            moveSpeedMult = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            turnRateVal = - turnRateMax;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnRateVal = turnRateMax;
        }
        else
        {
            turnRateVal = 0;
        }

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void FixedUpdate ()
    {
        momentum = Vector3.Lerp(momentum, (transform.forward * moveSpdMax * moveSpeedMult), .01f);

        angMomentum = Vector3.Lerp(angMomentum, Vector3.up * turnRateVal, .01f);

        GetComponent<Rigidbody>().MovePosition(transform.position + momentum);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles + angMomentum));
    }
}
