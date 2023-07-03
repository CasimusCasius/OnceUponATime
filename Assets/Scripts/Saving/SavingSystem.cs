using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Saving
{

    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            print("Saving to the "+ saveFile);
        }

        public void Load(string saveFile)
        {
            print("Loading from the " + saveFile);
        }

    }
}