using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkPlayerScript : Photon.MonoBehaviour {

    private GameObject myCamera;
    private GameObject reticle;

    private bool isAlive = true;
    public Vector3 netPos;
    public Quaternion netRot;

    public bool hit;
    public bool HIT
    {
        get
        {
            return hit;
        }
        set
        {
            if (value != hit && hit == false)
            {
                hit = value;
                StartCoroutine("Hit");
            }
            else if (value != hit)
            {
                hit = value;
            }
        }
    }

    private float smoother = 5;
    //float timer;

    //public NetworkManagerScript NMS;
    //public Queue<GameObject> bullets = new Queue<GameObject>(20);

    private GameObject turret;

    public Quaternion turretRot;

    //private bool fire;
    //public bool FIRE
    //{
    //    get
    //    {
    //        return fire;
    //    }
    //    set
    //    {
    //        if (value != fire)
    //        {

    //            if (!fire)
    //            {
    //                FireBullet();
    //            }
    //            fire = value;

    //        }
    //    }
    //}

    // Use this for initialization
    void Awake()
    {
        //NMS = GameObject.Find("NetworkManager").GetComponent<NetworkManagerScript>();
        turret = transform.Find("TurretComposite").gameObject;
    }


    // Use this for initialization
    void Start () {

        //Debug.Log(bullets.Count);
        //for (int i = 0; i < 20; i++)
        //{
        //    bullets.Enqueue(NMS.MakeABullet());
        //}

        if (photonView.isMine)
        {
            gameObject.name = "Me";

            GetComponent<ShipMovementScript>().enabled = true;
            GetComponentInChildren<TurretScript>().enabled = true;

            myCamera = transform.Find("Main Camera").gameObject;
            myCamera.SetActive(true);

            reticle = transform.Find("Reticle").gameObject;
            reticle.SetActive(true);
        }
        else
        {
            gameObject.name = "NetworkPlayer";
            StartCoroutine("Alive");
        }
	}

    void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(HIT);
            stream.SendNext(turret.transform.rotation);
            //stream.SendNext(FIRE);
        }
        else
        {
            netPos = (Vector3)stream.ReceiveNext();
            netRot = (Quaternion)stream.ReceiveNext();
            HIT = (bool)stream.ReceiveNext();
            turretRot = (Quaternion)stream.ReceiveNext();
            //FIRE = (bool)stream.ReceiveNext();
        }
    }


    // While alive - state machine
    IEnumerator Alive()
    {
        while(isAlive)
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, netPos, Time.deltaTime * smoother));
            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, netRot, Time.deltaTime * smoother));
            turret.transform.rotation = Quaternion.Slerp(transform.rotation, turretRot, Time.deltaTime * smoother);

            yield return null;
        }
    }

    IEnumerator Hit()
    {
        transform.Find("Ship").gameObject.SetActive(false);
        transform.Find("TurretComposite").gameObject.SetActive(false);
        GetComponent<CapsuleCollider>().enabled = false;

        yield return new WaitForSeconds(3);

        transform.Find("Ship").gameObject.SetActive(true);
        transform.Find("TurretComposite").gameObject.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = true;
        HIT = false;
    }

}
