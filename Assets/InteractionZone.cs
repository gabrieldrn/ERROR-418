using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionZone : MonoBehaviour
{
    public bool inside = false;
    private GameObject HUD;
    private Sprite buttonSprite;
    void OnTriggerEnter(Collider autre)
    {
        inside = true;
        
        if (transform.parent.GetComponent<ServerModel>().isHacked & transform.parent.GetComponent<ServerModel>().canBeFixed)
        {
            //Debug.Log("Player inside dehack zone");
            //Light renew = transform.parent.GetComponent<ServerModel>().GetLight();


            HUD = autre.GetComponent<FixingServer>().HUD;
            HUD.SetActive(true);

            if(Input.GetJoystickNames().Length != 0)
            {
                HUD.GetComponentInChildren<Text>().text = "Hit 'A' to defend the server !";
                buttonSprite = autre.GetComponent<FixingServer>().buttonController;
                
            }
            else
            {
                HUD.GetComponentInChildren<Text>().text = "Hit Enter to defend the server !";
                buttonSprite = autre.GetComponent<FixingServer>().buttonKeyboard;
            }

            HUD.GetComponentInChildren<Image>().sprite = buttonSprite;
            Debug.Log(Input.GetJoystickNames().Length);

            autre.GetComponent<FixingServer>().ServerModel = transform.parent.GetComponent<ServerModel>();

            /*if(autre.GetComponent<FixingServer>().isFinished())
            {
                renew.color = Color.green;
            }*/

            transform.parent.GetComponent<ServerModel>().isHacked = false;
            transform.parent.GetComponent<ServerModel>().timeLeftBeforeIrreparable = transform.parent.GetComponent<ServerModel>().TIME_BEFORE_IRREPARABLE;
            transform.parent.GetComponent<ServerModel>().playOnceHacked = true;
        }
    }
    void OnTriggerExit(Collider autre)
    {
        inside = false;
        //Debug.Log("Joueur quitte de la zone");

        HUD = autre.GetComponent<FixingServer>().HUD;
        HUD.SetActive(false);

        autre.GetComponent<FixingServer>().ResetQTE();
        autre.GetComponent<FixingServer>().ServerModel = null;
    }
}
