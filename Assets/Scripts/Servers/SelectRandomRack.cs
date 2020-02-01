using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomRack : MonoBehaviour
{
    [Header("Number of rack in this bundle : ")]
    public int NB_RACKS_IN_BUNDLE = 3;
    [Header("Time between each bundle selection : ")]
    public float TIME_BEFORE_NEW_SELECTION = 15.0f;

    // Selecting an index
    public int randomRackIndex;

    float timeLeft;

    void Start()
    {
        timeLeft = TIME_BEFORE_NEW_SELECTION;
    }

    void Update()
    {
        // Decreasing time left
        timeLeft -= Time.deltaTime;

        // Generating a random number if time left isn't 0
        if (timeLeft < 0)
        {
            randomRackIndex = selectedRackIndex();

            // Resetting time left
            timeLeft = TIME_BEFORE_NEW_SELECTION;
        }
    }

    /*
     * Output : return a random rack index
     */
    int selectedRackIndex()
    {            
        // Generating a random index
        randomRackIndex = Random.Range(0, NB_RACKS_IN_BUNDLE);
        return randomRackIndex;
    }
}
