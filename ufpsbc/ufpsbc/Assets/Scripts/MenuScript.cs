using MLAPI;
using MLAPI.Transports.UNET;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuScript : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject hud;
    public InputField inputField;

    private void Start()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        hud.SetActive(!menuPanel.activeSelf);
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = false;
        string[] args = Encoding.ASCII.GetString(connectionData).Split(';');
        string pwd = args[0];
        int team = Int32.Parse(args[1]);

        approve = pwd == "mygame";

        Debug.Log($"approval: {approve}");

        Transform spawnpoint = SpawnManager.Instance.GetSpawnPoint(team, true);

        Debug.Log($"{(team == 1 ? "TR" : "CT")} entrando na posição : {spawnpoint.position.x} {spawnpoint.position.y} {spawnpoint.position.z}");
        callback(true, null, approve, spawnpoint.position, spawnpoint.rotation);
    }

    public void Host()
    {
        //TODO: o Host tambem deve ter um time
        NetworkManager.Singleton.StartHost();

        menuPanel.SetActive(false);
        hud.SetActive(!menuPanel.activeSelf);
        GameManagerScript.Instance.StartHud();
    }

    private void Join(int team)
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            Debug.Log("input de endereço está nulo");
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
        }
        else
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = inputField.text;
        }

        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes($"mygame;{team}");
        NetworkManager.Singleton.StartClient();

        menuPanel.SetActive(false);
        hud.SetActive(!menuPanel.activeSelf);
        GameManagerScript.Instance.StartHud();
        GameManagerScript.Instance.ResetPositions();
    }

    public void JoinAsTerrorist()
    {
        Debug.Log("Entrando como terrorista");
        Join((int)Constants.TEAM.TERRORISTS);
    }

    public void JoinAsCounterTerrorist()
    {
        Debug.Log("Entrando como contraterrorista");
        Join((int)Constants.TEAM.COUNTERTERRORISTS);
    }
}