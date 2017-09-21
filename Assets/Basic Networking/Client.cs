using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour{
	public string ip;
	public int port = 9999;

	//Network information
	NetworkClient client;
	int reliableChannelId;

	List<string> clientLog = new List<string>();

	public void CreateClient(string ip, int port){
		this.ip = ip;
		this.port = port;
		CreateClient();
	}

	public void CreateClient(){
		clientLog.Add("Attempting to create client");
		ConnectionConfig config = new ConnectionConfig();

		// Config the Channels we will use
		reliableChannelId = config.AddChannel(QosType.Reliable);

		// Create the client ant attach the configuration
		client = new NetworkClient();
		client.Configure(config, 1);

		// Register the handlers for the different network messages
		RegisterHandlers();

		// Connect to the server
		client.Connect(ip, port);
	}

	public string GetIP(){
		return Network.player.ipAddress;
	}

	public List<string> GetClientLog(){
		return clientLog;
	}

	public void ClearClientLog(){
		clientLog.Clear();
	}

	// Register the handlers for the different message types
	void RegisterHandlers(){
		// Unity have different Messages types defined in MsgType
		client.RegisterHandler(NetworkMessageIDs.StringNetworkMessage, OnMessageReceived);
		client.RegisterHandler(MsgType.Connect, OnConnected);
		client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
	}

	void OnConnected(NetworkMessage netMessage){
		clientLog.Add("Connected to server: " + netMessage);
		// Do stuff when connected to the server
		StringNetworkMessage messageContainer = new StringNetworkMessage();
		messageContainer.message = "Hello server!";

		// Say hi to the server when connected
		client.Send(NetworkMessageIDs.StringNetworkMessage, messageContainer);
	}

	void OnDisconnected(NetworkMessage netMessage) {
		// Do stuff when disconnected from the server
		clientLog.Add("Disconnected from server: " + netMessage);
	}

	// Message received from the server
	void OnMessageReceived(NetworkMessage netMessage) {
		// You can send any object that inherence from MessageBase
		// The client and server can be on different projects, as long as the StringNetworkMessage or the class you are using have the same implementation on both projects
		// The first thing we do is deserialize the message to our custom type
		switch(netMessage.msgType){
			case NetworkMessageIDs.StringNetworkMessage:
				StringNetworkMessage msg = netMessage.ReadMessage<StringNetworkMessage>();
				clientLog.Add("Message recieved: " + msg.message);
			break;
			default:
				clientLog.Add("Message recieved: " + netMessage);
			break;
		}
	}

	public void SendPing(){
		StringNetworkMessage messageContainer = new StringNetworkMessage();
		messageContainer.message = "Ping!";
		client.Send(NetworkMessageIDs.StringNetworkMessage, messageContainer);
		Debug.Log(client.GetRTT());
		clientLog.Add("" + client.GetRTT());
	}

	public void Disconnect(){
		clientLog.Add("Disconnecting from server");
		client.Disconnect();
	}
}