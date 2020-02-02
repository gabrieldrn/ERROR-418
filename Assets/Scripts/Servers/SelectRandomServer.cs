using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomServer : MonoBehaviour
{
    public static int idDep = 0;
    [Header("Number of rack in this bundle : ")]
    public int NB_RACKS_IN_BUNDLE = 3;
    [Header("Number of servers in this rack : ")]
    public int NB_SERVERS_IN_RACK = 5;
    [Header("Time between each bundle selection : ")]
    public float TIME_BEFORE_NEW_SELECTION = 10.0f;
    [Header("Time between each hack : ")]
    public float TIME_BEFORE_NEW_HACK = 5.0f;
    [Header("HUD reference : ")]
    public HudController HUD;
    
    int randomRackIndex;
    int randomServerIndex;
    float bundleSelectTimeLeft;
    float serverSelectTimeLeft;
    bool selectAServer = false;
    string lightsTag = "ServerStatusLight";
    string interractTag = "TriggerZone";
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

            if (serverSelectTimeLeft < 0)
            {
                serverSelected = selectRandomServer(rackSelected);
                // Apply effect on selected server
                effectOnServerChosen(serverSelected);

                serverSelected.GetComponent<ServerModel>().isHacked = true;

                foreach (Transform child in serverSelected.transform)
                {
                    if (child.CompareTag(interractTag))
                    {
                        //Debug.Log("I'm in");
                        bool interaction = child.GetComponent<InteractionZone>().inside;
                        Light light = child.GetComponent<Light>();
                        if (interaction)
                        { // si l'on est dans la zone trigger du serveur piraté
                            //Debug.Log("Interaction du joueur avec un serveur piraté");
                            light.color = Color.green;
                            serverSelected.GetComponent<ServerModel>().isHacked = false;
                            serverSelected.GetComponent<ServerModel>().timeLeftBeforeIrreparable = serverSelected.GetComponent<ServerModel>().TIME_BEFORE_IRREPARABLE;
                        }
                    }
                }
                    
            
                // Resetting time left before choosing new server
                serverSelectTimeLeft = TIME_BEFORE_NEW_SELECTION;
                selectAServer = false;
            }
        }
    }

    /*
     * Output : return a random rack
     */
    GameObject selectedRackIndex()
    {
        bool canBeChoosed = false;
        do
        {
            // Generating a random index
            randomRackIndex = Random.Range(0, NB_RACKS_IN_BUNDLE);

            for (int i = 0; i < this.transform.GetChild(randomRackIndex).childCount; i++)
            {
                if (this.transform.GetChild(randomRackIndex).GetChild(i).gameObject.GetComponent<ServerModel>().canBeFixed)
                {
                    canBeChoosed = true;
                }
            }
        } while (!canBeChoosed);
        

        // Actually returning the GameObject
        return this.transform.GetChild(randomRackIndex).gameObject;
    }

    /* 
     * Selecting a random server in the rack selected
     */
    GameObject selectRandomServer(GameObject rack)
    {
        do
        {
            // Generating a random index
            randomServerIndex = Random.Range(0, NB_SERVERS_IN_RACK);
        } while (!rack.transform.GetChild(randomServerIndex).gameObject.GetComponent<ServerModel>().canBeFixed );
        

        // Actually returning the GameObject
        return rack.transform.GetChild(randomServerIndex).gameObject;
    }

    /* Adding effect to chosen server
     * 
     */
    void effectOnServerChosen(GameObject server)
    {
        server.GetComponent<ServerModel>().InitServer();
        foreach (Transform child in server.transform)
        {
            if (child.CompareTag(lightsTag))
            {
                Light light = child.GetComponent<Light>();
                light.color = Color.red;
            }
            if(child.CompareTag(interractTag))
            {
                child.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }

    public void serverDown()
    {
        HUD.ProgressServersDown();
    }

    public void createProgressBar(GameObject server)
    {
        HUD.ShowServerProgressBar(server);
    }
}
