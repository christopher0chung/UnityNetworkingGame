using UnityEngine;
using System.Collections;

public class MouseCursorScript : MonoBehaviour {

    public Camera myCam;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = myCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myCam.transform.position.y));

        //Debug.Log(pos);
        transform.position = pos;


    }
}
