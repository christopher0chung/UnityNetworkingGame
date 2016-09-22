using UnityEngine;
using System.Collections;

public class NetworkClientScript : Photon.MonoBehaviour {

    public Vector3 netPos;
    public Quaternion netRot;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

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

    [PunRPC]
    public void 

}
