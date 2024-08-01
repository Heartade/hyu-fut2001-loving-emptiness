using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public GameObject playerCamera;
    Vector3 offset;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the position offset by the camera's rotation
        Vector3 rot = playerCamera.transform.rotation.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        targetPosition = playerCamera.transform.position + Quaternion.Euler(rot) * offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        transform.LookAt(transform.position * 2 - playerCamera.transform.position);
    }
}
