using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    public bool inside = false;
    void OnTriggerEnter(Collider autre)
    {
        inside = true;
        
        if (transform.parent.GetComponent<ServerModel>().isHacked & transform.parent.GetComponent<ServerModel>().canBeFixed)
        {
            //Debug.Log("Player inside dehack zone");
            Light renew = transform.parent.GetComponent<ServerModel>().GetLight();
            renew.color = Color.green;
            transform.parent.GetComponent<ServerModel>().isHacked = false;
        }
    }
    void OnTriggerExit(Collider autre)
    {
        inside = false;
        //Debug.Log("Joueur quitte de la zone");
    }
}
