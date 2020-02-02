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
    public bool playOnceFixed;

    private GameObject progressBar;
    private bool firstLoop = true;

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
        InitServer();
        audio = GetComponent<AudioSource>();
    }

    public void InitServer()
    {
        GetLight().color = Color.green;
        playOnceHacked = true;
        playOnceDown = true;
        playOnceFixed = true;
        this.isHacked = false;
        this.canBeFixed = true;
        this.firstLoop = true;
        timeLeftBeforeIrreparable = TIME_BEFORE_IRREPARABLE;
    }

    public void Update()
    {
        if (this.isHacked)
        {
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

                if (this.canBeFixed)
                {
                    effectOnServerDead();
                    this.canBeFixed = false;
                    this.transform.parent.root.GetComponent<SelectRandomServer>().serverDown();
                    Destroy(this.progressBar);
                }
            }
            else
            {
                if(progressBar != null)
                {
                    progressBar.SetActive(true);
                    Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
                    progressBar.transform.position = namePose;
                    updateProgressBar();
                }
                else
                {
                    this.transform.parent.root.GetComponent<SelectRandomServer>().createProgressBar(gameObject);
                }

                if (firstLoop) //First loop, ignore
                {
                    timeLeftBeforeIrreparable -= 0.1f;
                    firstLoop = false;
                }
                else if(timeLeftBeforeIrreparable >= TIME_BEFORE_IRREPARABLE) //Repaired
                {
                    //Debug.Log(this.transform.parent.name + " => REPAIRED - time left : " + timeLeftBeforeIrreparable);
                    //Debug.Log(this.ToString());

                    FixServer();
                    timeLeftBeforeIrreparable = TIME_BEFORE_IRREPARABLE - 0.01f;
                    Destroy(this.progressBar);
                }
                else //Normal flow, decrease time left
                {
                    timeLeftBeforeIrreparable -= Time.deltaTime;
                }
            }
        }
        else
        {
            if(this.progressBar != null)
            {
                Destroy(this.progressBar);
            }
        }
    }

    public void setProgressBar(GameObject progressBar)
    {
        this.progressBar = progressBar;
    }

    public void addProgress(float progress)
    {

        if (this.isHacked) { timeLeftBeforeIrreparable += progress; }
    }

    public void updateProgressBar()
    {
        float progress = timeLeftBeforeIrreparable / TIME_BEFORE_IRREPARABLE;
        if (progress > 1f)
            progress = 1f;
        progressBar.GetComponentInChildren<ProgressBarController>().SetProgress(progress);
    }

    public float getProgress()
    {
        return timeLeftBeforeIrreparable;
    }

    public void FixServer()
    {
        if (playOnceFixed)
        {
            playFixedSound();
            playOnceFixed = false;
        }

        InitServer();
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