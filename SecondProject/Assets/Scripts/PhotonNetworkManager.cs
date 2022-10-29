using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

//public class PhotonNetworkManager : MonoBehaviour
public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text connectionText;
    [SerializeField]
    private InputField chattingInput;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private Camera sceneCamera;
    [SerializeField]
    private GameObject playerModel;

    [SerializeField]
    private GameObject serverWindow;
    [SerializeField]
    private GameObject messageWindow;
    [SerializeField]
    private GameObject chatWindow;

    [SerializeField]
    private InputField username;
    [SerializeField]
    private InputField roomName;
    [SerializeField]
    private InputField roomList;
    [SerializeField]
    private InputField messagesLog;

    private GameObject player;
    private Queue<string> messages;
    private const int messageCount = 10;
    private string nickNamePrefKey = "PlayerName";

    private GameObject visibleFalseCamera;
    //----------------------------------
    // Start is called before the first frame update
    void Start()
    {
        messages = new Queue<string>(messageCount);
        if (PlayerPrefs.HasKey(nickNamePrefKey))
        {
            username.text = PlayerPrefs.GetString(nickNamePrefKey);
        }

        PhotonNetwork.AutomaticallySyncScene = true;    // �ٸ� Ŭ���̾�Ʈ�� ���� ����ȭ

        if (!PhotonNetwork.IsConnected)
        {
            bool resConnectUsingSetting = PhotonNetwork.ConnectUsingSettings();
            string str = PhotonNetwork.NetworkClientState.ToString();
            connectionText.text = str;

            if (resConnectUsingSetting) {
                str = "[1:Success] " + str + " Connect to Photon successfully ... OnConnectedToMaster() �ݹ��Լ� ����";
            } else {
                str = "[1:Fail] " + str + " Failed to Connect to Photon ... OnDisconnected() �ݹ��Լ� ����";
            }
            Debug.Log(str);
        }

        // ���߿� SetActive�� false�� �ٲ��ַ���
        visibleFalseCamera = GameObject.Find("Main Camera");
        if (visibleFalseCamera == null) { Debug.Log("[1] ����ī�޶� ã�� ���� .."); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //----------------------------------
    // ���� ������ �����ߴ��� �����ߴ��� �ݹ��Լ��� Ȯ��
    // �����ϸ� OnConnectedToMaster() --> �κ����� JoinLobby()
    // �����ϸ� OnDisconnected()
    public override void OnConnectedToMaster()
    {
        bool resJoinLobby = PhotonNetwork.JoinLobby();
        string str = PhotonNetwork.NetworkClientState.ToString();
        connectionText.text = str;

        if (resJoinLobby) {
            str = "[2:Success] " + str + " Join to lobby successfully ... OnJoinedLobby() �ݹ��Լ� ����";
        } else {
            str = "[2:Fail] " + str + " Failed to joining lobby ...";
        }
        Debug.Log(str);

        serverWindow.SetActive(true);
        chatWindow.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("[OnFailedToConnectToPhoton] StatusCode: " + cause.ToString() + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public override void OnJoinedLobby()
    {
        serverWindow.SetActive(true);
        connectionText.text = "(" + SceneManager.GetActiveScene().name + ") " + PhotonNetwork.NetworkClientState.ToString();

        // ***
        //JoinRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> rooms)
    {
        roomList.text = "";
        foreach (RoomInfo room in rooms)
        {
            roomList.text += room.Name + "\n";
        }
    }

    // [Join to Room] ��ư�� Ŭ������ �� ����
    public void JoinRoom()
    {
        serverWindow.SetActive(false);
        chatWindow.SetActive(true);
        connectionText.text = "Joining room ... " + PhotonNetwork.NetworkClientState.ToString();

        PhotonNetwork.LocalPlayer.NickName = username.text;
        PlayerPrefs.SetString(nickNamePrefKey, username.text);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            MaxPlayers = 8
        };

        string str = PhotonNetwork.NetworkClientState.ToString();
        if (PhotonNetwork.IsConnectedAndReady) {
            bool resJoinOrCreateRoom = PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
            str = "[3:Success] (" + SceneManager.GetActiveScene().name + ") " + str;
        } else {
            str = "[3:Fail] PhotonNetwork connection is not ready, try restart it. " + str;
        }
        connectionText.text = str;
        Debug.Log(str);
    }

    //----------------------------------
    // �� ������ �����ߴ��� �����ߴ��� �ݹ��Լ��� Ȯ��
    // �����ϸ� OnJoinedRoom()
    // �����ϸ�
    public override void OnJoinedRoom()
    {
        connectionText.text = "(" + PhotonNetwork.CurrentRoom.Name + ") " + PhotonNetwork.NetworkClientState.ToString();

        Respawn(0.0f);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        serverWindow.SetActive(true);
        connectionText.text = "[OnJoinRandomFailed] Joined Room failed .. " + PhotonNetwork.NetworkClientState.ToString();
    }

    // ************************************************

    void Respawn(float spawnTime)
    {
        StartCoroutine(RespawnCoroutine(spawnTime));
    }

    IEnumerator RespawnCoroutine(float spawnTime)
    {
        yield return new WaitForSeconds(spawnTime);
        messageWindow.SetActive(true);

        // ĳ���� ���� ��ġ(Spawnpoints ����Ʈ����)
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        player = PhotonNetwork.Instantiate(playerModel.name, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation, 0);
        if (player != null) {
            visibleFalseCamera.SetActive(false);
        } else {
            Debug.Log("[4] Player character is NOT instantiated ...");
        }

        string strState = PhotonNetwork.NetworkClientState.ToString();
        string str = "[" + PhotonNetwork.LocalPlayer.NickName + "] is " + strState;
        connectionText.text = str;

        if (spawnTime == 0)
        {
            if (PhotonNetwork.IsMasterClient) {
                AddMessage("Player " + PhotonNetwork.LocalPlayer.NickName + " (MasterClient) Joined Game.");
            } else {
                AddMessage("Player " + PhotonNetwork.LocalPlayer.NickName + " Joined Game.");
            }
        }
        else
        {
            AddMessage("Player " + PhotonNetwork.LocalPlayer.NickName + " Respawned.");
        }
    }

    void AddMessage(string message)
    {
        photonView.RPC("AddMessage_RPC", RpcTarget.All, message);
    }


    // ************************************

    public override void OnPlayerLeftRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            AddMessage("Player " + other.NickName + " Left Game.");
        }
    }

    // **** Chatting
    public void SendChattingMessage()
    {
        AddMessage( PhotonNetwork.LocalPlayer.NickName + "] " + chattingInput.text);
        chattingInput.text = "";
    }
    
    [PunRPC]
    void AddMessage_RPC(string message)
    {
        messages.Enqueue(message);
        if (messages.Count > messageCount)
        {
            messages.Dequeue();
        }
        messagesLog.text = "";
        foreach (string m in messages)
        {
            messagesLog.text += m + "\n";
        }
    }

}
