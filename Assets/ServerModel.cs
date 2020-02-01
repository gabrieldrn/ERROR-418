using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerModel : MonoBehaviour, IComparable<ServerModel>
{
    [Header("Time between each hack : ")]
    public float TIME_BEFORE_IRREPARABLE = 10.0f;

    public Light lighting;
    public GameObject TriggerZone;
    public bool isHacked;
    public bool canBeFixed; 

    string lightsTag = "ServerStatusLight";
    float timeLeftBeforeIrreparable;

    public ServerModel(Light newLighting, GameObject newTriggerZone)
    {
        this.isHacked = false;
        this.lighting = newLighting;
        this.TriggerZone = newTriggerZone;
    }

    void Start()
    {
        this.canBeFixed = true;
        timeLeftBeforeIrreparable = TIME_BEFORE_IRREPARABLE;
    }

    public void Update()
    {
        if (this.isHacked)
        {
            timeLeftBeforeIrreparable -= Time.deltaTime;

            if(timeLeftBeforeIrreparable < 0)
            {
                effectOnServerDead();
                this.canBeFixed = false;
            }
        }
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

    /*
     * Adding effect to dead server
     */
    void effectOnServerDead()
    {
        Light light = this.GetLight();
        light.color = Color.black;
    }

}