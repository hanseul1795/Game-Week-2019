using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;

    [SerializeField] Vector3 offset = new Vector3(0, 6.1f, -5.39f);

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y -= target.position.y;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
        transform.LookAt(target);
    }
}
