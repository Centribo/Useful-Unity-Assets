using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

	public int port = 9999;
	public int maxConnections = 10;
	public string ip;

	//Network information
	int reliableChannelId;

	List<string> serverLog = new List<string>();

	// Use this for initialization
	void Start () {
		// Usually the server doesn't need to draw anything on the screen
		Application.runInBackground = true;
	}

	public bool CreateServer(int port, int maxConnections){
		this.port = port;
		this.maxConnections = maxConnections;
		return CreateServer();
	}

	//Returns true if server created, false otherwise
	public bool CreateServer(){
		serverLog.Add("Attempting to create server");
		// Register handlers for the types of messages we can receive
		RegisterHandlers ();

		ConnectionConfig config = new ConnectionConfig();
		// There are different types of channels you can use, check the official documentation
		reliableChannelId = config.AddChannel(QosType.Reliable);
		
		HostTopology ht = new HostTopology(config, maxConnections);

		if(!NetworkServer.Configure(ht)){
			serverLog.Add("No server created, error on the configuration definition");
			return false;
		} else {
			// Start listening on the defined port
			if(NetworkServer.Listen(port)){
				serverLog.Add("Server created, listening on port: " + port);
				return true;
			} else {
				serverLog.Add("No server created, could not listen to the port: " + port);    
				return false;
			}
		}
	}

	public void DestroyServer(){
		serverLog.Add("Server destoryed");
		NetworkServer.Shutdown();
	}

	public string GetIP(){
		//return Network.player.ipAddress;
		return Network.player.externalIP;
	}

	public List<string> GetServerLog(){
		return serverLog;
	}

	public void ClearServerLog(){
		serverLog.Clear();
	}

	private void RegisterHandlers(){
		// Default Unity messages
		NetworkServer.RegisterHandler (MsgType.Connect, OnClientConnected);
		NetworkServer.RegisterHandler (MsgType.Disconnect, OnClientDisconnected);

		// Our message use his own message type.
		NetworkServer.RegisterHandler (NetworkMessageIDs.StringNetworkMessage, OnMessageReceived);
	}

	private void RegisterHandler(short t, NetworkMessageDelegate handler) {
		NetworkServer.RegisterHandler (t, handler);
	}

	void OnApplicationQuit(){
		NetworkServer.Shutdown();
	}

	void OnClientConnected(NetworkMessage netMessage) {
		// Do stuff when a client connects to this server
		serverLog.Add("Client connected: " + netMessage.conn);

		// Send a thank you message to the client that just connected
		StringNetworkMessage messageContainer = new StringNetworkMessage();
		messageContainer.message = "Thanks for joining!";

		// This sends a message to a specific client, using the connectionId
		NetworkServer.SendToClient(netMessage.conn.connectionId,NetworkMessageIDs.StringNetworkMessage,messageContainer);

		// Send a message to all the clients connected
		messageContainer = new StringNetworkMessage();
		messageContainer.message = "A new player has conencted to the server";

		// Broadcast a message a to everyone connected
		NetworkServer.SendToAll(NetworkMessageIDs.StringNetworkMessage,messageContainer);
	}

	void OnClientDisconnected(NetworkMessage netMessage){
		// Do stuff when a client dissconnects
		serverLog.Add("Client disconnected: " + netMessage.conn);
	}

	void OnMessageReceived(NetworkMessage netMessage){
		switch(netMessage.msgType){
			case NetworkMessageIDs.StringNetworkMessage:
				StringNetworkMessage msg = netMessage.ReadMessage<StringNetworkMessage>();
				serverLog.Add("Message recieved: " + msg.message);
			break;
			default:
				serverLog.Add("Message recieved: " + netMessage);
			break;
		}
	}
}