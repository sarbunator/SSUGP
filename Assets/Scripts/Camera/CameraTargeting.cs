using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTargeting : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;







    void Update()
    {
        CamTarget();
    }



    private void CamTarget()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, cam.nearClipPlane));
        Vector3 targetPosition = (player.position + mousePosition) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -threshold + player.position.x, threshold + player.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -threshold + player.position.y, threshold + player.position.y);
        targetPosition.z = this.transform.position.z;

        this.transform.position = targetPosition;
    }

    private void OldCamTarget()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = (player.position + mousePosition) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -threshold + player.position.x, threshold + player.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPosition;
    }
}
