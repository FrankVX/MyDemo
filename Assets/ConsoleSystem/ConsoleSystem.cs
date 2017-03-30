
using System;
using UnityEngine;

namespace MC.CheatNs
{
    public class ConsoleSystem : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                UIConsoleSystem.GetInstance.Active();
            }
        }

    }
}