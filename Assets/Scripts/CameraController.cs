using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Reference to the songMetadata script
    public SongMetadata songData;

    public Camera mainCamera;

    public Camera orbitCamera;

    public GameObject avatar;

    public Conductor conductor;

    Vector3 defaultCameraPosition;

    Vector3 targetCameraPosition;
    Quaternion targetCameraRotation;

    public string setPosition;

    public bool cameraIsMoving;

    // Start is called before the first frame update
    void Start()
    {
        defaultCameraPosition = mainCamera.transform.localPosition;

        targetCameraPosition = defaultCameraPosition;
        targetCameraRotation = Quaternion.Euler(0, 0, 0);

        //Debug.Log(defaultCameraPosition.x);
        //Debug.Log(defaultCameraPosition.y);
        //Debug.Log(defaultCameraPosition.z);

        cameraIsMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkCameraCues();

        moveCameraPosition(4f, setPosition);
    }

    public void moveCameraPosition(float speed, string position)
    {
        if (cameraIsMoving == false)
        {
            setCameraTargetPosition(position);

            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, targetCameraPosition, Time.deltaTime * speed);

            mainCamera.transform.localRotation = Quaternion.Lerp(mainCamera.transform.localRotation, targetCameraRotation, Time.deltaTime * speed);
        }
    }

    void snapToCameraPosition(string position)
    {
        setCameraTargetPosition(position);

        mainCamera.transform.localPosition = targetCameraPosition;
        mainCamera.transform.localRotation = targetCameraRotation;
    }

    void spinCamera(float x, float y, float speed)
    {
        if (cameraIsMoving == true)
        {
            Vector3 axis = new Vector3(x, y, 0f);

            mainCamera.transform.RotateAround(avatar.transform.localPosition, axis, speed * Time.deltaTime);
        }
    }

    void setCameraTargetPosition(string position)
    {
        if (position == "center_near")
        {
            cameraIsMoving = false;
            targetCameraPosition.Set(-1.5f, -5.99f, -10.9f);
            targetCameraRotation = Quaternion.Euler(0, 0, 0);
        }

        if (position == "center_far")
        {
            cameraIsMoving = false;
            targetCameraPosition.Set(-1.5f, -5.99f, -12.5f);
            targetCameraRotation = Quaternion.Euler(0, 0, 0);
        }

        if (position == "low_left_near")
        {
            cameraIsMoving = false;
            targetCameraPosition.Set(-1.9f, -6.66f, -10.1f);
            targetCameraRotation = Quaternion.Euler(-14f, 15, 0);
        }

        if (position == "low_right_near")
        {
            cameraIsMoving = false;
            targetCameraPosition.Set(1.9f, -6.66f, -10.1f);
            targetCameraRotation = Quaternion.Euler(-14f, -15, 0);
        }
    }

    void checkCameraCues()
    {
        for (int i = 0; i < SongMetadata.cam_center_near.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_center_near[i])
            {
                setPosition = "center_near";
            }
        }

        for (int i = 0; i < SongMetadata.cam_center_far.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_center_far[i])
            {
                setPosition = "center_far";
            }
        }

        for (int i = 0; i < SongMetadata.cam_low_left_near.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_low_left_near[i])
            {
                setPosition = "low_left_near";
            }
        }

        for (int i = 0; i < SongMetadata.cam_low_right_near.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_low_right_near[i])
            {
                setPosition = "low_right_near";
            }
        }
    }
}
