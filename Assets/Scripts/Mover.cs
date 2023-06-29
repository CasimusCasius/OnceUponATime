using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        GetComponent<Animator>().SetFloat("forwardSpeed", localVelocity.z);
    }

    private void MoveToCursor()
    {
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(Ray, out RaycastHit hitInfo))
        {
            GetComponent<NavMeshAgent>().destination = hitInfo.point;
        }

    }
}
