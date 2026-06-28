using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Sensitivity")]
    public float mouseSensitivity = 0.3f;
    public float touchSensitivity = 0.1f;

    [Header("Vertical Clamp")]
    public float minVertical = -80f;
    public float maxVertical = 80f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private Vector3 lastMousePos;

    void Start()
    {
        // No longer locking the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        HandleMouseDrag();
        HandleTouch();
    }

    void HandleMouseDrag()
    {
        // Only rotate while left mouse button is held
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;

            rotationY += delta.x * mouseSensitivity;
            rotationX -= delta.y * mouseSensitivity;
            rotationX = Mathf.Clamp(rotationX, minVertical, maxVertical);

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);

            lastMousePos = Input.mousePosition;
        }
    }

    public void SetInitialRotation(Vector3 rotation)
    {
        rotationX = rotation.x;
        rotationY = rotation.y;
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    }

    void HandleTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY += touch.deltaPosition.x * touchSensitivity;
                rotationX -= touch.deltaPosition.y * touchSensitivity;
                rotationX = Mathf.Clamp(rotationX, minVertical, maxVertical);

                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
            }
        }
    }
}