using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }

    public void StartRotating()
    {
        isRotating = true;
    }

    public void StopRotating()
    {
        isRotating = false;
    }
}