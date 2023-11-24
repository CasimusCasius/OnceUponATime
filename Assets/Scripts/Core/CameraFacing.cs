using UnityEngine;

namespace Game.Core
{
    public class CameraFacing : MonoBehaviour
    {
        

        private void Update()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
