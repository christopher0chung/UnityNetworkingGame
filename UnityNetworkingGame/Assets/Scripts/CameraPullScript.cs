using UnityEngine;
using System.Collections;

public class CameraPullScript : MonoBehaviour {

    private Vector3 closePos;
    private Vector3 farPos;

	// Use this for initialization
	void Start () {

        closePos = new Vector3(0, 50, 17.65f);
        farPos = new Vector3(0, 100, 38.86f);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.localPosition = Vector3.Lerp(closePos, farPos, transform.root.gameObject.GetComponent<ShipMovementScript>().momentumMag);
	
	}
}
