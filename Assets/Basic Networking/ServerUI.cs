using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Server))]
public class ServerUI : MonoBehaviour {

	public Text serverLogText;
	public Text ipText;
	public InputField portField;
	public InputField maxConnectionsField;

	Server server;

	void Awake(){
		server = GetComponent<Server>();
	}

	public void CreateServer(int port, int maxConnections){
		if(server.CreateServer(port, maxConnections)){
			ipText.text = server.GetIP();
		}
	}

	public void CreateServer(){
		int port;
		int maxConnections;

		if(!int.TryParse(portField.text, out port)){
			Debug.Log("Invalid port number");
			return;
		}

		if(!int.TryParse(maxConnectionsField.text, out maxConnections)){
			Debug.Log("Invalid max connections");
			return;
		}

		this.CreateServer(port, maxConnections);
	}

	// Update is called once per frame
	void Update () {
		List<string> serverLog = server.GetServerLog();
		string formatted = "";
		foreach(string s in serverLog){
			formatted += s + "\n";
		}
		serverLogText.text = formatted;
	}
}
