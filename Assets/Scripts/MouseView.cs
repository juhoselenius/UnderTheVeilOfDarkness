using Logic.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseView : MonoBehaviour
{
    private float mouseSensitivity;
    public IOptionsManager _ioptionsManager;
    private bool reverseMouse;
    public Transform playerObject;
    private float mouseY;
    float xRotation = 0f;

    private void Awake()
    {
        _ioptionsManager = ServiceLocator.GetService<IOptionsManager>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = _ioptionsManager.getMouseSensitivity();
        reverseMouse = _ioptionsManager.getReverseMouse();
    }
   
    void Update()
    {
        _=(reverseMouse) ? (mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime) : (mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime);
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerObject.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        _ioptionsManager.mouseSensitivityChanged += UpdateMouseSensitivity;
        _ioptionsManager.reverseMouseChanged += UpdateReverseMouse;
        UpdateMouseSensitivity(_ioptionsManager.getMouseSensitivity());
        UpdateReverseMouse(_ioptionsManager.getReverseMouse());
    }

    private void OnDisable()
    {
        _ioptionsManager.mouseSensitivityChanged -= UpdateMouseSensitivity;
    }

    void UpdateMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }

    void UpdateReverseMouse(bool value)
    {
        reverseMouse = value;
    }
}
