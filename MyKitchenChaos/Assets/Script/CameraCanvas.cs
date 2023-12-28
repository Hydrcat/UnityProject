using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraCanvas : MonoBehaviour
{
    private enum Mode{
        FaceToCamera,
        FaceToCameraInvert,
        Forward,
        ForwardInvert,
    }

     [SerializeField] private Mode mode; 

     private void Update() {
        switch (mode)
        {
            case Mode.FaceToCamera:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.FaceToCameraInvert:
                Vector3 PositionDir = Camera.main.transform.position - transform.position;
                transform.LookAt(transform.position+PositionDir);
                break;
            case Mode.Forward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.ForwardInvert:
                transform.forward = -Camera.main.transform.forward;
                break;

        }
     }
}
