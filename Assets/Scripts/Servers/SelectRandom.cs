using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandom : MonoBehaviour
{
    [Header("Number of servers in this rack : ")]
    public int NB_SERVERS_IN_RACK = 5;
    [Header("Time between each hack : ")]
    public float TIME_BEFORE_NEW_HACK = 15.0f;

    float timeLeft;
    int randomServerIndex;

    void Start()
    {
        timeLeft = TIME_BEFORE_NEW_HACK;
    }

    void Update()
    {
        selectingRandomServer();
    }

    /* Selecting a random server in the rack
     * Output : return chosen server
     * Additional infos : 
     *  - Range of random number in (0, NB_SERVERS_IN_RACK)
     *  - choose a new server after TIME_BEFORE_NEW_HACK seconds
     */
    void selectingRandomServer()
    {
        // Decreasing time left
        timeLeft -= Time.deltaTime;

        // Generating a random number if time left isn't 0
        if (timeLeft < 0)
        {
            // Generating a random index
            randomServerIndex = Random.Range(0, NB_SERVERS_IN_RACK);

            // Actually returning the GameObject
            // this.getServer(randomServerIndex);
            Debug.Log(this.getServer(randomServerIndex).name);

            // Resetting time left
            timeLeft = TIME_BEFORE_NEW_HACK;
        }
    }

    /* Return the child of a server array
     * Input : index of the object to get
     * Output : the GameObject
     */
    GameObject getServer(int index)
    {
        return this.transform.GetChild(index).gameObject;
    }
}
