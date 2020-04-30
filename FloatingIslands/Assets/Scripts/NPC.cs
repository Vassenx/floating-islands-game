using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private static int idCount;
    public int id;

    void Start()
    {
        id = idCount++;
    }

    void Update()
    {
        
    }
}
