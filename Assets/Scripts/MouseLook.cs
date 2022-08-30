using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }

    [SerializeField]
    private float horizontalSensitivity = 9.0f;
    [SerializeField]
    private float verticalSensitivity = 9.0f;
    private float minVert = -45.0f;
    private float maxVert = 45.0f;

    private float rotationX = 0.0f;
    public RotationAxes axes = RotationAxes.MouseXAndY;

    private bool pausedGame = false;
    private void Awake() 
    {
        Messenger.AddListener(GameEvents.GAME_PAUSED, onGamePaused);
        Messenger.AddListener(GameEvents.GAME_RESUMED, onGameUnpaused);
    }

    // Update is called once per frame
    void Update()
    {
        if(!pausedGame)
        {
            if(axes == RotationAxes.MouseX)
            {
                transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * horizontalSensitivity);
            } else if(axes == RotationAxes.MouseY) 
            {
                rotationX -= Input.GetAxis("Mouse Y") * verticalSensitivity;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

                transform.localEulerAngles = new Vector3(rotationX, 0, 0);
            } else
            {
                rotationX -= Input.GetAxis("Mouse Y") * verticalSensitivity;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

                float deltaHorizontal = Input.GetAxis("Mouse X") * horizontalSensitivity;
                float rotationY = transform.localEulerAngles.y + deltaHorizontal;

                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

            }
        }
    }

    void onGamePaused()
    {
        pausedGame = true;
    }

    void onGameUnpaused()
    {
        pausedGame = false;
    }
}
