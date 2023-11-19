using Game.Saving;
using System.Collections;
using UnityEngine;

namespace Game.SceneManagement
{
    public class SavingWraper : MonoBehaviour
    {
        const string DEAFULT_SAVEFILE = "autosave";

        [SerializeField] float fadeInTime = 0.2f;

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }
        private IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(DEAFULT_SAVEFILE);
            var fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
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
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
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

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(DEAFULT_SAVEFILE);
        }
    }
}