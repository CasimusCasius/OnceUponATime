using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Game.SceneManagement
{
	public class Portal : MonoBehaviour
	{
        [SerializeField] private int sceneToLoadIndex = -1;
        [SerializeField] private Transform spawnPoint;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {

            
            yield return SceneManager.LoadSceneAsync(sceneToLoadIndex);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal.spawnPoint);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in GameObject.FindObjectsOfType<Portal>())
            {
                if (portal == this) { continue; }
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