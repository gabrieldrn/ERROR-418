using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomServer : MonoBehaviour
{
    [Header("Number of servers in this rack : ")]
    public int NB_SERVERS_IN_RACK = 5;
    [Header("Time between each hack : ")]
    public float TIME_BEFORE_NEW_HACK = 15.0f;

    float timeLeft;
    int randomServerIndex;
    GameObject selectedServer;
    SelectRandomRack parentScript;

    void Start()
    {
        // Get selecting rack script
        parentScript = transform.parent.GetComponent<SelectRandomRack>();
        timeLeft = TIME_BEFORE_NEW_HACK;
    }

    void Update()
    {
        if (this.isSelected())
        {
            // Decreasing time left
            timeLeft -= Time.deltaTime;

            // Generating a random number if time left isn't 0
            if (timeLeft < 0)
            {
                selectedServer = selectingRandomServer();
                selectedServer.GetComponent<Renderer>().material.color = new Color(255f, 0f, 0f, 1f);

                // Resetting time left
                timeLeft = TIME_BEFORE_NEW_HACK;
            }
        }
    }

    /* Selecting a random server in the rack
     * Output : return chosen server
     * Additional infos : 
     *  - Range of random number for selection in (0, NB_SERVERS_IN_RACK)
     */
    GameObject selectingRandomServer()
    {
        // Generating a random index
        randomServerIndex = Random.Range(0, NB_SERVERS_IN_RACK);

        // Actually returning the GameObject
        return this.transform.GetChild(randomServerIndex).gameObject;
    }


    /* If selected index in parent equal to this child index return true
     *
     */
    public bool isSelected()
    {
        if (parentScript.randomRackIndex == transform.GetSiblingIndex())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
