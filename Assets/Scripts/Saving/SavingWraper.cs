using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Saving
{
    public class SavingWraper : MonoBehaviour
    {
        const string DEAFULT_SAVEFILE = "autosave.sav";

        private void Update()
        {
            var savingSystem = GetComponent<SavingSystem>();

            if (savingSystem == null) return;
            
            if (Input.GetKeyDown(KeyCode.F5))
            {
                savingSystem.Save(DEAFULT_SAVEFILE);
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                savingSystem.Load(DEAFULT_SAVEFILE);
            }
        }
    }
}