using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AmazingUI
{
    public class ToggleAnimatorController : MonoBehaviour
    {

        Toggle toggle;
        public Animator animator;
        public string booleanParameter = "Pressed";

        void Start()
        {
            toggle = this.GetComponent<Toggle>();

            if (toggle)
            {
                toggle.onValueChanged.RemoveListener(Switch);
                toggle.onValueChanged.AddListener(Switch);
            }
            else
            {
                print("Script does not work. Please add this script to the Toggle.");
            }
        }

        void Switch(bool value)
        {
            if (toggle)
            {
                if (value) animator.GetComponent<Animator>().SetBool(booleanParameter, true);
                else animator.GetComponent<Animator>().SetBool(booleanParameter, false);
            }
        }
    }
}

