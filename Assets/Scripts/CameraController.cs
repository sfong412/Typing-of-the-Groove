using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;

    public Camera orbitCamera;

    public GameObject avatar;

    Vector3 defaultCameraPosition;

    Vector3 targetCameraPosition;
    Quaternion targetCameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        defaultCameraPosition = mainCamera.transform.localPosition;

        targetCameraPosition = defaultCameraPosition;
        targetCameraRotation = Quaternion.Euler(0, 0, 0);

        Debug.Log(defaultCameraPosition.x);
        Debug.Log(defaultCameraPosition.y);
        Debug.Log(defaultCameraPosition.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            snapToCameraPosition("center_near");
        }

        if (Input.GetKey(KeyCode.K))
        {
            spinCamera(0.2f, 1f, 14f);
        }

        if (Input.GetKey(KeyCode.J))
        {
            moveCameraPosition(4f, "low_right_near");
        }

        if (Input.GetKey(KeyCode.H))
        {
            moveCameraPosition(4f, "low_left_near");
        }

        if (Input.GetKey(KeyCode.P))
        {
            moveCameraPosition(4f, "center_far");
        }
    }

    void moveCameraPosition(float speed, string position)
    {
        setCameraTargetPosition(position);

        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, targetCameraPosition, Time.deltaTime * speed);

        mainCamera.transform.localRotation = Quaternion.Lerp(mainCamera.transform.localRotation, targetCameraRotation, Time.deltaTime * speed);
    }

    void snapToCameraPosition(string position)
    {
        setCameraTargetPosition(position);

        mainCamera.transform.localPosition = targetCameraPosition;
        mainCamera.transform.localRotation = targetCameraRotation;
    }

    void spinCamera(float x, float y, float speed)
    {
        Vector3 axis = new Vector3(x, y, 0f);

        mainCamera.transform.RotateAround(avatar.transform.localPosition, axis, speed * Time.deltaTime);
    }

    void setCameraTargetPosition(string position)
    {
        if (position == "center_near")
        {
            targetCameraPosition.Set(-1.5f, -5.99f, -10.9f);
            targetCameraRotation = Quaternion.Euler(0, 0, 0);
        }

        if (position == "center_far")
        {
            targetCameraPosition.Set(-1.5f, -5.99f, -12.5f);
            targetCameraRotation = Quaternion.Euler(0, 0, 0);
        }

        if (position == "low_left_near")
        {
            targetCameraPosition.Set(-1.9f, -6.66f, -10.1f);
            targetCameraRotation = Quaternion.Euler(-14f, 15, 0);
        }

        if (position == "low_right_near")
        {
            targetCameraPosition.Set(1.9f, -6.66f, -10.1f);
            targetCameraRotation = Quaternion.Euler(-14f, -15, 0);
        }
    }
}
