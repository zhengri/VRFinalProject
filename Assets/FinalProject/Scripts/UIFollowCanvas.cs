using UnityEngine;

public class UIFollowCanvas : MonoBehaviour
{
    public Transform vrCamera;       // Assign the XR Camera
    public Vector3 offset = new Vector3 (0, 0, 100 ); // x meters in front

    void LateUpdate()
    {
        if (vrCamera == null) return;

        // Position canvas in front of camera
        transform.position = vrCamera.position + vrCamera.forward * offset.z
                             + vrCamera.up * offset.y
                             + vrCamera.right * offset.x;

        // Make canvas face camera
        transform.rotation = Quaternion.LookRotation(transform.position - vrCamera.position);
    }
}
