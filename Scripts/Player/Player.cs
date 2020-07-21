using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Quest currentQuest;
    public Shovel shovel;
    public QuestTarget currentTarget = null;
    public Key optainedKey = null;

    public Transform shovelMarker;
    public Transform objectSpawnMarker;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
