using UnityEngine;
using System.Collections;
using AMC.Networking;
using System;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ServerNetworkController : NetworkController {

    [SerializeField]
    private EventManager EventManager;

    private List<int> ConnectionIds;

    private List<Match> Matches;

    void Start()
    {
        ConnectionIds = new List<int>();
    }

    public override void StartClient()
    {
        
    }

    public override void StartClient(string ipAdress)
    {
        
    }

    public override void StartHosting()
    {
        CreateServerContext<AmcServer>();
    }

    protected override void AddHandlers()
    {
       
    }

    protected override void OnConnectionReceived(NetworkMessage msg)
    {
        base.OnConnectionReceived(msg);
        ConnectionIds.Add(msg.conn.connectionId);
        Debug.Log(ConnectionIds);        
    }


    protected override void OnDisconnect(NetworkMessage msg)
    {
        base.OnDisconnect(msg);
        ConnectionIds.Remove(msg.conn.connectionId);
    }


    private void Relay()
    {

    }
}
