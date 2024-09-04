using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotaitor : MonoBehaviour
{
    [SerializeField] private float rotationSpead;

    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpead * Time.deltaTime, 0));
    }
}
