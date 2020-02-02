using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndZoneTrigger : MonoBehaviour
{
    public string NextLevel;
    public bool active = false;

    void OnTriggerEnter(Collider autre)
    {
        if (active && NextLevel != null)
        {
            SceneManager.LoadScene(NextLevel);
        }
      
    }
}
