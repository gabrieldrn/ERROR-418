using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerModel : MonoBehaviour, IComparable<ServerModel>
{
    public Light lighting;
    public GameObject TriggerZone;
    public bool isHacked;
    public ServerModel(Light newLighting, GameObject newTriggerZone)
    {
        this.isHacked = false;
        this.lighting = newLighting;
        this.TriggerZone = newTriggerZone;
    }

    public void Update()
    {

    }

    public int CompareTo(ServerModel other)
    {
        if (other == null)
            return 1;
        return 0;
    }

    public Light GetLight()
    {
        return this.lighting;
    }


}