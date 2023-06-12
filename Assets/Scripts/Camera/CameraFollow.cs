using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;

    void Update()
    {
        transform.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y, transform.position.z);
        transform.rotation = Quaternion.identity;
    }
}
