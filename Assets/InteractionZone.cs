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
                Debug.Log(HUD);
                HUD.GetComponentInChildren<Text>().text = "Hit 'A' to defend the server !";
                buttonSprite = autre.GetComponent<FixingServer>().buttonController;
                
            }
            else
            {
                HUD.GetComponentInChildren<Text>().text = "Hit Enter to defend the server !";
                buttonSprite = autre.GetComponent<FixingServer>().buttonKeyboard;
            }

            HUD.GetComponentInChildren<Image>().sprite = buttonSprite;

            autre.GetComponent<FixingServer>().ServerModel = transform.parent.GetComponent<ServerModel>();

            /*if(autre.GetComponent<FixingServer>().isFinished())
            {
                renew.color = Color.green;
            }*/
        }
    }
    void OnTriggerExit(Collider autre)
    {
        inside = false;
        //Debug.Log("Joueur quitte de la zone");

        HUD = autre.GetComponent<FixingServer>().HUD;
        HUD.SetActive(false);

        autre.GetComponent<FixingServer>().ServerModel = null;
    }
}
