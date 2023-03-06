using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRaycast : MonoBehaviour
{
    public LayerMask interactable;
    public Camera playerCamera;

    public RaycastHit ShootRay(float p_RayDistance)
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, p_RayDistance, interactable)) {
            return hitData;
        }
        return hitData;
    }
}
