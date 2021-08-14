using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    SpawnPointGroup TR_SpawnPoints;
    SpawnPointGroup CT_SpawnPoints;

    //essa constante existe para preservar a informação de em qual time o player recem conectado vai entrar
    Constants.TEAM nextPlayerTeam;

    void Awake()
    {
        Instance = this;
        TR_SpawnPoints = GetComponentsInChildren<SpawnPointGroup>().FirstOrDefault(x=>x.name.Contains("_TR"));
        //Debug.Log(("TR spawnpoints " + (TR_SpawnPoints != null ? "OK" : "NOK")));

        CT_SpawnPoints = GetComponentsInChildren<SpawnPointGroup>().FirstOrDefault(x=>x.name.Contains("_CT"));
        //Debug.Log(("CT spawnpoints " + (CT_SpawnPoints != null ? "OK" : "NOK")));

        Constants.TEAM nextPlayerTeam = Constants.TEAM.UNASSIGNED;
    }

    //Pega o spawnpoint do time apropriado
    public Transform GetSpawnPoint(int team, bool firstConnection = false)
    {
        switch ((Constants.TEAM) team)
        {
            case Constants.TEAM.TERRORISTS:
            case Constants.TEAM.UNASSIGNED:
                nextPlayerTeam = Constants.TEAM.TERRORISTS;
                return this.TR_SpawnPoints.GetNextSpawnPoint(firstConnection);
            case Constants.TEAM.COUNTERTERRORISTS:
                nextPlayerTeam = Constants.TEAM.COUNTERTERRORISTS;
                return this.CT_SpawnPoints.GetNextSpawnPoint(firstConnection);
            default:
                nextPlayerTeam = Constants.TEAM.UNASSIGNED;
                return null;
        }
    }

    //Aqui o spawnManager associa um player a um time
    public Constants.TEAM GetNextTeamAssignment()
    {
        Constants.TEAM nextTeam =  this.nextPlayerTeam;
        nextPlayerTeam = Constants.TEAM.UNASSIGNED;

        return nextTeam;
    }
}
