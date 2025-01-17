using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class InputHandler : MonoBehaviour
    {
        #region Data
            [BoxGroup("Input Data")]
            public CameraInputData cameraInputData;
            [BoxGroup("Input Data")]
            public MovementInputData movementInputData;
            [BoxGroup("Input Data")]
            public InteractionInputData interactionInputData;
        #endregion

        #region BuiltIn Methods

            void start()
            {
                cameraInputData.ResetInput();
                movementInputData.ResetInput();
                interactionInputData.ResetInput();
            }

            void update()
            {
                GetCameraInput();
                GetMovementInputData();
                GetInteractionInputData();
            }
        #endregion

        #region Custom Methods

            void GetInteractionInputData()
            {
                interactionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
                interactionInputData.InteractedRelease = Input.GetKeyUp(KeyCode.E);
            }
            void GetCameraInput()
            {
                cameraInputData.InputVectorX = Input.GetAxis("Mouse X");
                cameraInputData.InputVectorY = Input.GetAxis("Mouse Y");

                cameraInputData.ZoomClicked = Input.GetMouseButtonDown(1);
                cameraInputData.ZoomReleased = Input.GetMouseButtonUp(1);
            }

            void GetMovementInputData()
            {
                movementInputData.InputVectorX = Input.GetAxisRaw("Horizontal");
                movementInputData.InputVectorY = Input.GetAxisRaw("Vertical");
            }
        #endregion
    }
}