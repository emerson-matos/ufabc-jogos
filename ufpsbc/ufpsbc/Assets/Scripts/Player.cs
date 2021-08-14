using UnityEngine;
using static Constants;

public class Player : MonoBehaviour
{
    public TEAM team;
    public bool isAlive;
    private string location = "respawn";
    public bool isInBox;

    private void Awake()
    {
        team = SpawnManager.Instance.GetNextTeamAssignment();
    }
        
    public void ResetarPosicao()
    {
        Transform spawnpoint = SpawnManager.Instance.GetSpawnPoint((int)this.team);
        var ph = GetComponent<PlayerHealth>();

        ph.RespawnClientRpc(spawnpoint.position, spawnpoint.rotation);
    }

    void Update()
    {
        if (isInBox)
        {
            location = "bombsite";            
        }
        else
        {
            location = "somewhere";
        }
    }

    public TEAM GetTEAM()
    {
        return team;
    }

    public string getPlayerLocation()
    {
        return location;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bombsite")){
            Debug.Log("Pisou no Bombsite");
            isInBox = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bombsite")){
            Debug.Log("Saiu do Bombsite");
            isInBox = false;
        }
    }
}
