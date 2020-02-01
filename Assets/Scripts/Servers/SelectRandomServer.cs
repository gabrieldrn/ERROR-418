using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomServer : MonoBehaviour
{
    [Header("Number of rack in this bundle : ")]
    public int NB_RACKS_IN_BUNDLE = 3;
    [Header("Number of servers in this rack : ")]
    public int NB_SERVERS_IN_RACK = 5;
    [Header("Time between each bundle selection : ")]
    public float TIME_BEFORE_NEW_SELECTION = 10.0f;
    [Header("Time between each hack : ")]
    public float TIME_BEFORE_NEW_HACK = 5.0f;


    int randomRackIndex;
    int randomServerIndex;
    float bundleSelectTimeLeft;
    float serverSelectTimeLeft;
    bool selectAServer = false;
    GameObject rackSelected;
    GameObject serverSelected;

    void Start()
    {
        bundleSelectTimeLeft = TIME_BEFORE_NEW_SELECTION;
        serverSelectTimeLeft = TIME_BEFORE_NEW_HACK;
    }

    void Update()
    {
        // Decreasing time left
        bundleSelectTimeLeft -= Time.deltaTime;

        // Generating a random number if time left isn't 0
        if (bundleSelectTimeLeft < 0)
        {
            rackSelected = selectedRackIndex();
            selectAServer = true;

            // Resetting time left
            bundleSelectTimeLeft = TIME_BEFORE_NEW_SELECTION;
        }

        // If a server has to be selected
        if (selectAServer)
        {
            serverSelectTimeLeft -= Time.deltaTime;

            if(serverSelectTimeLeft < 0)
            {
                serverSelected = selectRandomServer(rackSelected);
                // Apply effect on selected server
                effectOnServerChosen(serverSelected);

                // Resetting time left before choosing new server
                serverSelectTimeLeft = TIME_BEFORE_NEW_SELECTION;
            }
        }
    }

    /*
     * Output : return a random rack
     */
    GameObject selectedRackIndex()
    {            
        // Generating a random index
        randomRackIndex = Random.Range(0, NB_RACKS_IN_BUNDLE);

        // Actually returning the GameObject
        return this.transform.GetChild(randomRackIndex).gameObject;
    }

    /* 
     * Selecting a random server in the rack selected
     */
    GameObject selectRandomServer(GameObject rack)
    {
        // Generating a random index
        randomServerIndex = Random.Range(0, NB_SERVERS_IN_RACK);

        // Actually returning the GameObject
        return rack.transform.GetChild(randomServerIndex).gameObject;
    }

    /* Adding effect to chosen server
     * 
     */
    void effectOnServerChosen(GameObject server)
    {
        server.GetComponent<Renderer>().material.color = new Color(255f, 0f, 0f, 1f);
    }

    /*
     * Adding effect to dead server
     */
    void effectOnServerDead(GameObject server)
    {
        server.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 1f);
    }
}
