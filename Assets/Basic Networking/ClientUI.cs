using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Client))]
public class ClientUI : MonoBehaviour {

	public Text clientLogText;
	public InputField ipField;
	public InputField portField;

	Client client;

	void Awake(){
		client = GetComponent<Client>();
	}

	public void CreateClient(string ip, int port){
		client.CreateClient(ip, port);
	}

	public void CreateClient(){
		int port;

		if(!int.TryParse(portField.text, out port)){
			Debug.Log("Invalid port number");
			return;
		}

		this.CreateClient(ipField.text, port);
	}

	public void Disconnect(){
		client.Disconnect();
	}

	// Update is called once per frame
	void Update () {
		List<string> clientLog = client.GetClientLog();
		string formatted = "";
		foreach(string s in clientLog){
			formatted += s + "\n";
		}
		clientLogText.text = formatted;
	}
}
