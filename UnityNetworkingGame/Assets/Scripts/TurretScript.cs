using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretScript : MonoBehaviour {

    public Transform target;

    //public GameObject ammo;

    public Queue<GameObject> bullets = new Queue<GameObject>(20);

    public NetworkManagerScript NMS;
    //public NetworkPlayerScript NPS;

    //Use this for initialization

   void Awake ()
    {
        NMS = GameObject.Find("NetworkManager").GetComponent<NetworkManagerScript>();
        //NPS = transform.root.gameObject.GetComponent<NetworkPlayerScript>();
    }

    void Start ()
    {
        Debug.Log(bullets.Count);
        for (int i = 0; i < 20; i++)
        {
            bullets.Enqueue(NMS.MakeABullet());
        }

    }
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(target);

        if (Input.GetMouseButtonDown(0))
        {
            //GameObject myBullet = (GameObject) Instantiate(ammo, ((transform.forward * 3) + (transform.position)), transform.rotation);
            //GameObject thisBullet = NMS.MakeABullet();
            //thisBullet.transform.rotation = Quaternion.Euler(0, 180, 0);

            GameObject myBullet = bullets.Dequeue();
            myBullet.transform.position = transform.position + transform.forward * 6;
            myBullet.transform.rotation = transform.rotation;
            myBullet.GetComponent<BulletScript>().enabled = true;
            myBullet.GetComponent<BulletScript>().timer = 0;
            myBullet.GetComponent<SphereCollider>().enabled = true;

            bullets.Enqueue(myBullet);
        }

	}
}
