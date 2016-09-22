using UnityEngine;
using System.Collections;

public class BulletScript : Photon.MonoBehaviour {

    public float timer;

    private bool isAlive = true;
    public Vector3 netPos;
    public Quaternion netRot;

    private float smoother = 5;

    public bool isMineIs;

    // Use this for initialization
    void Start () {
        GetComponent<SphereCollider>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (timer >= 5)
        {
            transform.position = new Vector3(0, 100, 0);
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<BulletScript>().enabled = false;
        }

        if (!photonView.isMine)
        {
            isMineIs = false;
            StartCoroutine("Alive");
        }
        else
        {
            isMineIs = true;
            GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * 1.5f);

            timer += Time.deltaTime;
        }

	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            netPos = (Vector3)stream.ReceiveNext();
            netRot = (Quaternion)stream.ReceiveNext();
        }
    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "NetworkPlayer")
        {
            other.gameObject.GetComponent<NetworkPlayerScript>().HIT = true;
        }
        else if (other.name == "Me")
        {
            other.gameObject.GetComponent<NetworkPlayerScript>().HIT = true;
        }
    }

    // While alive - state machine
    IEnumerator Alive()
    {
        while (isAlive)
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, netPos, Time.deltaTime * smoother));
            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, netRot, Time.deltaTime * smoother));

            yield return null;
        }
    }
}
