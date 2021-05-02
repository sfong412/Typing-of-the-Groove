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

    Vector3 defaultMainCameraPosition;

    Vector3 defaultOrbitCameraPosition;

    Quaternion defaultOrbitCameraRotation;

    Vector3 targetCameraPosition;
    Quaternion targetCameraRotation;

    public string setPosition;

    public bool cameraIsMoving;

    public bool cameraSnap;

    bool orbitCameraUsed;

    // Start is called before the first frame update
    void Start()
    {
        defaultMainCameraPosition = mainCamera.transform.localPosition;
        defaultOrbitCameraPosition = orbitCamera.transform.localPosition;
        defaultOrbitCameraRotation = orbitCamera.transform.localRotation;

        targetCameraPosition = defaultMainCameraPosition;
        targetCameraRotation = Quaternion.Euler(0, 0, 0);

        //Debug.Log(defaultCameraPosition.x);
        //Debug.Log(defaultCameraPosition.y);
        //Debug.Log(defaultCameraPosition.z);

        cameraIsMoving = false;
        cameraSnap = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkCameraCues();
        resetOrbitCameraPosition();

        if (cameraIsMoving == true && cameraSnap == false)
        {
            spinCamera(0.1f, 1f, 15f, setPosition);
        }

        else if (cameraIsMoving == true && cameraSnap == true)
        {
            //snapToCameraPosition(setPosition);
            spinCamera(0.1f, 1f, 15f, setPosition);
        }

        else if (cameraIsMoving == false && cameraSnap == false)
        {
            moveCameraPosition(4f, setPosition);
        }

        else if (cameraIsMoving == false && cameraSnap == true)
        {
            snapToCameraPosition(setPosition);
        }
    }

    public void moveCameraPosition(float speed, string position)
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

    void spinCamera(float x, float y, float speed, string position)
    {
        Vector3 axis = new Vector3(x, y, 0f);

        if (setPosition == "spin_ccw")
        {
            speed = -speed;
        }

        if (cameraSnap == true && orbitCameraUsed == true)
        {
            orbitCamera.transform.RotateAround(avatar.transform.localPosition, axis, speed * Time.deltaTime);
            mainCamera.transform.localPosition = orbitCamera.transform.localPosition;
            mainCamera.transform.localRotation = orbitCamera.transform.localRotation;
        }
        else
        {
            mainCamera.transform.RotateAround(avatar.transform.localPosition, axis, speed * Time.deltaTime);
        }
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
            targetCameraPosition.Set(-0.4f, -6.66f, -10.1f);
            targetCameraRotation = Quaternion.Euler(-14f, -15, 0);
        }

        if (position == "spin" || position == "spin_ccw")
        {
            targetCameraPosition.Set(-1.6f, -5.99f, -10.7f);
            targetCameraRotation = Quaternion.Euler(14f, 0, 0);
        }
    }

    void checkCameraCues()
    {
        for (int i = 0; i < SongMetadata.cam_center_near.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_center_near[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = false;
                cameraSnap = false;
                setPosition = "center_near";
            }
        }

        for (int i = 0; i < SongMetadata.cam_center_far.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_center_far[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = false;
                cameraSnap = false;
                setPosition = "center_far";
            }
        }

        for (int i = 0; i < SongMetadata.cam_low_left_near.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_low_left_near[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = false;
                cameraSnap = false;
                setPosition = "low_left_near";
            }
        }

        for (int i = 0; i < SongMetadata.cam_low_right_near.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_low_right_near[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = false;
                cameraSnap = false;
                setPosition = "low_right_near";
            }
        }

        for (int i = 0; i < SongMetadata.cam_spin.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_spin[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = true;
                cameraSnap = false;
                setPosition = "spin";
            }
        }

        for (int i = 0; i < SongMetadata.cam_spin_ccw.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_spin_ccw[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = true;
                cameraSnap = false;
                setPosition = "spin_ccw";
            }
        }

        for (int i = 0; i < SongMetadata.cam_center_near_snap.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_center_near_snap[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = false;
                cameraSnap = true;
                setPosition = "center_near";
            }
        }

        for (int i = 0; i < SongMetadata.cam_center_far_snap.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_center_far_snap[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = false;
                cameraSnap = true;
                setPosition = "center_far";
            }
        }

        for (int i = 0; i < SongMetadata.cam_low_left_near_snap.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_low_left_near_snap[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = false;
                cameraSnap = true;
                setPosition = "low_left_near";
            }
        }

        for (int i = 0; i < SongMetadata.cam_low_right_near_snap.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_low_right_near_snap[i])
            {
                mainCamera.enabled = true;
                orbitCamera.enabled = false;

                orbitCameraUsed = false;
                cameraIsMoving = false;
                cameraSnap = true;
                setPosition = "low_right_near";
            }
        }

        for (int i = 0; i < SongMetadata.cam_spin_snap.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_spin_snap[i])
            {
                mainCamera.enabled = false;
                orbitCamera.enabled = true;

                orbitCameraUsed = true;
                cameraIsMoving = true;
                cameraSnap = true;
                setPosition = "spin";
            }
        }

        for (int i = 0; i < SongMetadata.cam_spin_snap_ccw.Length; i++)
        {
            if (conductor.completedBeats == SongMetadata.cam_spin_snap_ccw[i])
            {
                mainCamera.enabled = false;
                orbitCamera.enabled = true;

                orbitCameraUsed = true;
                cameraIsMoving = true;
                cameraSnap = true;
                setPosition = "spin_ccw";
            }
        }
    }

    void resetOrbitCameraPosition()
    {
        if (orbitCameraUsed == false)
        {
            orbitCamera.transform.localPosition = defaultOrbitCameraPosition;
            orbitCamera.transform.localRotation = defaultOrbitCameraRotation;
        }
    }
}
