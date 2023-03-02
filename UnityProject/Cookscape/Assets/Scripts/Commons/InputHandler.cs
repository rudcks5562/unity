using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FPS Tutorial 참조
//유저의 Input 핸들링 용
namespace UnityProject.Cookscape
{
    public class InputHandler : MonoBehaviour
    {
        [Tooltip("Sensitivity multiplier for moving the camera around")]
        public float LookSensitivity = 1f;

        [Tooltip("Additional sensitivity multiplier for WebGL")]
        public float WebglLookSensitivityMultiplier = 0.25f;

        [Tooltip("Limit to consider an input when using a trigger on a controller")]
        public float TriggerAxisThreshold = 0.4f;

        [Tooltip("Used to flip the vertical input axis")]
        public bool InvertYAxis = false;

        [Tooltip("Used to flip the horizontal input axis")]
        public bool InvertXAxis = false;

        float lastXInput = 0;
        float lastYInput = 0;

        // Start is called before the first frame update
        void Start()
        {
            //Corsur Status => Locked
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //지금 인풋 받아도 되는지 체크
        public bool CanProcessInput()
        {
            //Cursor Status is Locked ? True : False
            return Cursor.lockState == CursorLockMode.Locked;
        }

        //얼마나 이동하는지 받기
        public Vector3 GetMoveInput()
        {
            lastXInput = 0;
            lastYInput = 0;

            if (CanProcessInput())
            {
                lastXInput = Input.GetAxisRaw(GameConstants.axisNameHorizontal);
                lastYInput = Input.GetAxisRaw(GameConstants.axisNameVertical);

                Vector3 move = new Vector3(lastXInput, 0f, lastYInput);

                move = Vector3.ClampMagnitude(move, 1);
                return move;
            }

            //인풋 금지
            return Vector3.zero;
        }

        public bool HasHorizonalPlusInput()
        {
            return lastXInput > 0;
        }

        public bool HasHorizonalMinusInput()
        {
            return lastXInput < 0;
        }

        public bool HasVerticalPlusInput()
        {
            return lastYInput > 0;
        }

        public bool HasVerticalMinusInput()
        {
            return lastYInput < 0;
        }

        float GetMouseLookAxis(string mouseInputName)
        {
            if (CanProcessInput())
            {
                float val = Input.GetAxisRaw(mouseInputName);

                if ((InvertYAxis && mouseInputName.Equals(GameConstants.mouseAxisNameVertical))
                   || (InvertXAxis && mouseInputName.Equals(GameConstants.mouseAxisNameHorizontal)))
                {
                    val *= -1f;
                }

                val *= LookSensitivity;

                val *= 0.01f;
#if UNITY_WEBGL
                    // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
                    val *= WebglLookSensitivityMultiplier;
#endif
                return val;
            }

            return 0f;
        }

        //좌우 시점이동 받기
        public float GetLookInputHorizontal()
        {
            return GetMouseLookAxis(GameConstants.mouseAxisNameHorizontal);
        }

        //세로 시점이동 받기
        public float GetLookInputVertical()
        {
            return GetMouseLookAxis(GameConstants.mouseAxisNameVertical);
        }

        //키 Input Downs ================================================
        public bool GetJumpInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetButtonDown(GameConstants.buttonNameJump);
            }

            return false;
        }

        public bool GetRunKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.LeftShift);
            }
            return false;
        }

        public bool GetCrouchKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.LeftControl);
            }
            return false;
        }

        public bool GetAttackKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.Mouse0);
            }
            return false;
        }

        public bool GetEKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.E);
            }
            return false;
        }

        public bool GetQKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.Q);
            }
            return false;
        }

        public bool GetFKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.F);
            }
            return false;
        }

        public bool GetCKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.C);
            }
            return false;
        }

        public bool GetMKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.M);
            }
            return false;
        }

        public bool GetBackQuoteKeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.BackQuote);
            }
            return false;
        }

        public bool GetBackNumber1KeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.Alpha1);
            }
            return false;
        }

        public bool GetBackNumber2KeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.Alpha2);
            }
            return false;
        }

        public bool GetBackNumber3KeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.Alpha3);
            }
            return false;
        }

        public bool GetBackNumber4KeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.Alpha4);
            }
            return false;
        }

        public bool GetBackNumber5KeyInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyDown(KeyCode.Alpha5);
            }
            return false;
        }

        //Key Input Helds ======================================================
        public bool GetJumpInputHeld()
        {
            if (!CanProcessInput())
            {
                return Input.GetButton(GameConstants.buttonNameJump);
            }

            return false;
        }

        public bool GetCrouchKeyInputHeld()
        {
            if (CanProcessInput())
            {
                return Input.GetKey(KeyCode.LeftControl);
            }

            return false;
        }

        public bool GetRunKeyInputHeld()
        {
            if (CanProcessInput())
            {
                return Input.GetKey(KeyCode.LeftShift);
            }

            return false;
        }

        public bool GetAltInputHeld()
        {
            if (CanProcessInput())
            {
                return Input.GetKey(KeyCode.LeftAlt);
            }

            return false;
        }

        //Key Input Ups ======================================================
        public bool GetAltInputUp()
        {
            if (CanProcessInput())
            {
                return Input.GetKeyUp(KeyCode.LeftAlt);
            }
            return false;
        }

    }
}
