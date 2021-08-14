using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointGroup : MonoBehaviour
{
    SpawnPoint[] spawnPoints;
    private int nextSpawnPointId;
    private int connectedPlayers;
    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        nextSpawnPointId = 0;
        connectedPlayers = 0;
    }

    public Transform GetNextSpawnPoint(bool firstConnection = false)
    {
        //significa que acabou as vagas
        //TODO: tratar isso depois usando uma nova Exception
        if (connectedPlayers >= this.spawnPoints.Length)
            return null;

        //pega o primeiro spawnpoint que não está usado para evitar que players do mesmo time tenham o mesmo ponto de spawn
        SpawnPoint sp = this.spawnPoints[nextSpawnPointId++];

        if(nextSpawnPointId >= this.spawnPoints.Length)
        {
            nextSpawnPointId = 0;
        }
        if (firstConnection)
        {
            connectedPlayers++;
        }
        return sp.transform;
    }
}
