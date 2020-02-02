using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixingServer : MonoBehaviour
{
    public GameObject loadingBar;
    private int speed; 
    private float currentAmount;
    float timestamp = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        currentAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            timestamp = Time.time + 0.2f;
            currentAmount += speed;
        }
        if (Time.time >= timestamp) {
            currentAmount -= (speed*2) * Time.deltaTime; 
        }
        if(currentAmount / 100 != 1){
            loadingBar.transform.GetComponent<Image>().fillAmount = currentAmount / 100;
        }
    }
}
