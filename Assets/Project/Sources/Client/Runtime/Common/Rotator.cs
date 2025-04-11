
using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float _rotationSpeed;

    private float _currentRotation = 0f;

    private void Update()
    {
        _currentRotation -= Time.deltaTime * _rotationSpeed;
        transform.rotation = Quaternion.Euler(0, _currentRotation, 0);
    }

    public void ResetRotation() => _currentRotation = 0f;
}
