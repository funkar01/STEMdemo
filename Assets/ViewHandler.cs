using UnityEngine;

public class ViewHandler : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] float rotationSpeed = 100f;
    bool isDraggable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDraggable==true)
            // Rotate the camera around the center of the scene (0, 0, 0) as per the user's mouse drag input    
            if (Input.GetMouseButton(0)) // Left mouse button is held down
            {
                float horizontalInput = Input.GetAxis("Mouse X"); // Get horizontal mouse movement
                float verticalInput = Input.GetAxis("Mouse Y"); // Get vertical mouse movement
                                                                // Rotate around the Y-axis for horizontal movement and X-axis for vertical movement
                mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
                mainCamera.transform.RotateAround(Vector3.zero, mainCamera.transform.right, -verticalInput * rotationSpeed * Time.deltaTime);
            }
    }

    public void SetDraggable(bool isActive)
    {
        isDraggable = isActive;
        Debug.Log($"Camera Draggable: {isDraggable}");
    }
}
