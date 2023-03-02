using UnityEngine;

public class RaycastTester : MonoBehaviour
{
    [Tooltip("Raycast가 반응할 LayerMask")]
    LayerMask layerMask;
    [Tooltip("Raycast를 쏠 카메라")]
    Camera m_RaycastCamera;

    const float RAYCAST_DISTANCE = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_RaycastCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = m_RaycastCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 rayDirection = m_RaycastCamera.transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection);

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, RAYCAST_DISTANCE, layerMask))
        {
            Debug.Log(hitInfo.transform.name);
        }
    }
}
