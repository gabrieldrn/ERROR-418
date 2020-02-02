using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndZoneTrigger : MonoBehaviour
{
    public bool active = false;

    void OnTriggerEnter(Collider autre)
    {
        if (active)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
    public void SetActive()
    {
        active = true;
    }
}
