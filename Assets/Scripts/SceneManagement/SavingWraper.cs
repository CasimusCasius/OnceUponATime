using Game.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SceneManagement
{
    public class SavingWraper : MonoBehaviour
    {
        const string DEAFULT_SAVEFILE = "autosave";

        [SerializeField] float fadeInTime = 0.2f;

        private IEnumerator Start()
        {
            var fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(DEAFULT_SAVEFILE);
            yield return fader.FadeIn(fadeInTime);
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.F5))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(DEAFULT_SAVEFILE);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(DEAFULT_SAVEFILE);
        }
    }
}