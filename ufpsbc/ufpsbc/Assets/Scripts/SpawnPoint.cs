using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject graphics;
    public bool inUse;
    void Awake()
    {
        graphics.SetActive(false);
    }
}
