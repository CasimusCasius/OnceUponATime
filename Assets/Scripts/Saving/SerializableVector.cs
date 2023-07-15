using UnityEngine;

namespace Game.Saving
{
    [System.Serializable]
    public class SerializableVector
    {
        float x, y, z;

        public SerializableVector(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }

        public Vector3 ToVector() 
        {
        
            return new Vector3(x, y, z);
        }

    }
}
