using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Game.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum EDestinationIdentifier
        {
            A, B, C, D, E, F, G
        }

        [SerializeField] private int sceneToLoadIndex = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private EDestinationIdentifier destination;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoadIndex < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }
            DontDestroyOnLoad(gameObject);
            
            Fader fader = FindAnyObjectByType<Fader>();

            yield return fader.FadeOut(fadeOutTime);
            SaveAtCheckpoint();
            yield return SceneManager.LoadSceneAsync(sceneToLoadIndex);

            LoadAtCheckpoint();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal.spawnPoint);

            SaveAtCheckpoint();
            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private static void LoadAtCheckpoint()
        {
            SavingWraper savingWraper = FindObjectOfType<SavingWraper>();
            if (savingWraper != null)
                savingWraper.Load();
        }

        private static void SaveAtCheckpoint()
        {
            SavingWraper savingWraper = FindObjectOfType<SavingWraper>();
            if (savingWraper != null)
            {
                savingWraper.Save();
            }
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in GameObject.FindObjectsOfType<Portal>())
            {
                if (portal == this || portal.destination != this.destination) { continue; }
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Transform portalSpawnPoint)
        {
            var player = GameObject.FindWithTag("Player");

            player.GetComponent<NavMeshAgent>().Warp(portalSpawnPoint.position);
            player.transform.rotation = portalSpawnPoint.rotation;
        }
    }
}