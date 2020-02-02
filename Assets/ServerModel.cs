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
    public float timeLeftBeforeIrreparable;
    public AudioClip serverFixed;
    public AudioClip serverHacked;
    public AudioClip serverDown;
    public bool playOnceHacked;
    public bool playOnceDown;

    string lightsTag = "ServerStatusLight";
    AudioSource audio;

    public ServerModel(Light newLighting, GameObject newTriggerZone)
    {
        this.isHacked = false;
        this.lighting = newLighting;
        this.TriggerZone = newTriggerZone;
    }

    void Start()
    {
        this.canBeFixed = true;
        playOnceHacked = true;
        playOnceDown = true;
        timeLeftBeforeIrreparable = TIME_BEFORE_IRREPARABLE;
        audio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (this.isHacked)
        {
            timeLeftBeforeIrreparable -= Time.deltaTime;

            if (playOnceHacked)
            {
                audio.clip = serverHacked;
                audio.Play();
                playOnceHacked = false;
            }

            if(timeLeftBeforeIrreparable < 0)
            {
                if (playOnceDown)
                {
                    audio.clip = serverDown;
                    audio.Play();
                    playOnceDown = false;
                }

                effectOnServerDead();
                this.canBeFixed = false;
                this.isHacked = false;
                this.transform.parent.root.GetComponent<SelectRandomServer>().serverDown();
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

    public void playFixedSound()
    {
        audio.clip = serverFixed;
        audio.Play();
    }

}