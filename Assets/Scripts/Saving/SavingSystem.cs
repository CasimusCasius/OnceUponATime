using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Saving
{

    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            print("Saving to the "+ GetPathFromSaveFile(saveFile));
        }

        public void Load(string saveFile)
        {
            print("Loading from the " + GetPathFromSaveFile(saveFile));
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine( Application.persistentDataPath,saveFile);
        }


    }


}