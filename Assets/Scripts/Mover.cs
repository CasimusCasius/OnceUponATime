using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask layerMask;

    Ray lastRay;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(lastRay, out RaycastHit hitInfo))
        {
            GetComponent<NavMeshAgent>().destination = hitInfo.point;
        }
    }
}
