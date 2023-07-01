using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.SceneManagement
{
	public class Portal : MonoBehaviour
	{
        [SerializeField] private int sceneToLoadIndex = -1;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                print("Portal triggered");
                SceneManager.LoadScene(sceneToLoadIndex);
            }
        }
    }

}