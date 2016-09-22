using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

    private string VERSION = "v0.0.2";

    private string roomName = "myName";
    private string playerPrefab = "ShipObj";
    private string playersBullet = "Bullet";

    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings(VERSION);
        //Debug.Log("Start run.");
	}

    void OnJoinedLobby()
    {
        //Debug.Log("Joined Lobby.");
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        //Debug.Log("Join or create attempted.");
    }

    void OnJoinedRoom ()
    {
        //Debug.Log("Joined Room.");
        PhotonNetwork.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, 0);
    }

    public GameObject MakeABullet()
    {
        Debug.Log("Make a bullet");
        return PhotonNetwork.Instantiate(playersBullet, Vector3.up * 100, Quaternion.identity, 0);
    }

}
