using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;

    public Camera orbitCamera;

    public GameObject avatar;

    Vector3 defaultCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        defaultCameraPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            snapToDefaultCameraPosition();
        }

        if(Input.GetKey(KeyCode.K))
        {
            spinCamera(0.2f, 1f, 8f);
        }

        if(Input.GetKey(KeyCode.J))
        {
            moveToDefaultCameraPosition(4f);
        }
    }

    void snapToDefaultCameraPosition()
    {
        mainCamera.transform.position = defaultCameraPosition;
        mainCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }


    //clean up camera movement
    void moveToDefaultCameraPosition(float speed)
    {
        float x = 1f;
        float y = 1f;
        float z = 1f;

        float rotateX = 1f;
        float rotateY = 1f;
        float rotateZ = 1f;

        if (mainCamera.transform.position.x > defaultCameraPosition.x)
        {
            x = -x;
        }

        if (mainCamera.transform.position.y > defaultCameraPosition.y)
        {
            y = -y;
        }

        if (mainCamera.transform.position.z > defaultCameraPosition.z)
        {
            z = -z;
        }

        if (mainCamera.transform.rotation.x > 0f)
        {
            rotateX = -rotateX;
        }

        if (mainCamera.transform.position.y > 0f)
        {
            rotateY = -rotateY;
        }

        if (mainCamera.transform.position.z > 0f)
        {
            rotateZ = -rotateZ;
        }

        Quaternion target = Quaternion.Euler(rotateX, rotateY, rotateZ);
        Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);

        while (mainCamera.transform.position != defaultCameraPosition)
        {
            mainCamera.transform.position = mainCamera.transform.position + new Vector3(x * Time.deltaTime * speed, y * Time.deltaTime * speed, z * Time.deltaTime * speed);
            break;
        }

        while (mainCamera.transform.rotation != defaultRotation)
        {
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, target, Time.deltaTime * speed);
            break;
        }
    }

    void spinCamera(float x, float y, float speed)
    {
        Vector3 axis = new Vector3(x, y, 0f);

        mainCamera.transform.RotateAround(avatar.transform.position, axis, speed * Time.deltaTime);
    }
}
