using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class PlayerHealth : NetworkBehaviour
{
    private static float _MaxHealth = 100f;
    public float MaxHealth { get => _MaxHealth; }
    public NetworkVariableFloat health = new NetworkVariableFloat(_MaxHealth);
    MeshRenderer[] renderers;
    CharacterController cc;
    Player player;

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        cc = GetComponent<CharacterController>();
        player = GetComponent<Player>();
        player.isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        health.Value -= damage;

        //checkHealth
        if(health.Value <= 0 && player.isAlive)
        {
            player.isAlive = false;
            Debug.Log($"respawn de jogador do time:{(player.team == Constants.TEAM.TERRORISTS ? "TR" : "CT")}");

            //Aqui pegamos o próximo spawnPoint de quando o player nasce
            Transform spawnpoint = SpawnManager.Instance.GetSpawnPoint((int) player.team, false);

            //respawn
            Debug.Log($"posição respawn: {spawnpoint.position.x} {spawnpoint.position.y} {spawnpoint.position.z}");
            RespawnClientRpc(spawnpoint.position, spawnpoint.rotation);
        }
    }
    
    [ClientRpc]
    public void RespawnClientRpc   (Vector3 position, Quaternion rotation)
    {
        StartCoroutine(Respawn(position, rotation));
    }

    IEnumerator Respawn(Vector3 position, Quaternion rotation)
    {      

        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }

        yield return new WaitForSeconds(1f);
        health.Value = 100;
        cc.enabled = false;

        //delay 
        transform.position = position;
        transform.rotation = rotation;
        cc.enabled = true;
        player.isAlive = true;

        foreach (var renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
}
