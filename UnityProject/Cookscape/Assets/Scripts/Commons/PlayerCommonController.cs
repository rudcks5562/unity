using UnityEngine;

namespace UnityProject.Cookscape
{
    public class PlayerCommonController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("플레이어의 메인카메라")]
        public Transform PlayerCamera;

        [Tooltip("걷는 소리")]
        public AudioSource PlayerWalkingAudioSource;

        [Tooltip("달리는 소리")]
        public AudioSource PlayerRunningAudioSource;

        [Tooltip("점프 소리")]
        public AudioSource PlayerJumpingAudioSource;

        [Tooltip("플레이어의 rigidbody")]
        public Rigidbody PlayerBody;

        [Tooltip("발 체크")]
        public Transform FeetTransform;

        [Tooltip("Check FloorLayer")]
        public LayerMask FloorMask;

        Animator PlayerAnimator;
        InputHandler inputHandler;
        //CharacterController characterController;


        //너 땅에 닿아있니?
        public bool IsGround { get; private set; }
        //지금 점프했니
        public bool HasJumpedThisFrame { get; private set; }
        //땅에 닿아있다고 판정하는 거리
        [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
        public float GroundCheckDistance = 0.05f;
        //초기 카메라 세로 각도
        public float CameraVerticalAngle = 20f;
        //초기 카메라 가로 각도
        public float CameraHorizonalAngle = 0f;
        //최대 속도
        public float MaxSpeedOnGround = 10f;
        //캐릭터 속도
        public Vector3 CharacterVelocity { get; set; }

        Vector3 GroundNormal;
        //마지막으로 점프한 시간
        float LastJumpedTime = -10;

        bool IsRunning = false;
        bool IsCrouched = false;

        //하늘에 있을 때 땅에 닿아있다고 판정하는 거리
        const float GroundCheckDistanceInAir = 0.07f;
        //최소한 하늘에 떠 있어야 하는 시간
        const float JumpGroundingPreventionTime = 0.2f;
        //회전 속도
        [SerializeField] float RotationSpeed = 20f;
        //점프 속도
        [SerializeField] float JumpForce = 9f;
        //이동 속도
        [SerializeField] float MovementSpeed = 10f;
        float ModifiedMovementSpeed
        {
            get
            {
                if (IsRunning) return MovementSpeed * 2f;
                if (IsCrouched) return MovementSpeed * 0.3f;
                return MovementSpeed;
            }
        }
        //For Animator
        float GravityForAnimator
        {
            get
            {
                if (IsRunning) return 2.0f;
                if (IsCrouched) return 0.5f;
                return 1.0f;
            }
        }

        public float rotationMultiplier
        {
            get
            {
                return 1f;
            }
        }

        private void Awake()
        {
            //characterController = GetComponent<CharacterController>();
            inputHandler = GetComponent<InputHandler>();
            PlayerAnimator = GetComponent<Animator>();

        }

        // Start is called before the first frame update
        void Start()
        {
            IsGround = true;
        }

        // Update is called once per frame
        void Update()
        {

            if (transform.position.y < -10)
            {
                Debug.Log("야 지금 너무 낮아");
            }

            HasJumpedThisFrame = false;

            bool wasGrounded = IsGround;
            CheckGround();
            //PlayerAnimator.SetBool(GameConstants.playerOnGround, IsGround);

            //땅에 막 부딪힌 순간
            if (IsGround && !wasGrounded)
            {
                PlayerAnimator.SetBool(GameConstants.playerJumpNow, false);
                Debug.Log("착지!");
            }

            //달리기&앉기를 만들다면?
            if (inputHandler.GetRunKeyInputDown())
            {
                IsRunning = true;
                IsCrouched = false;
                Debug.Log("달려!");
            }
            else if (inputHandler.GetCrouchKeyInputDown())
            {
                IsRunning = false;
                IsCrouched = true;
                Debug.Log("웅크려");
            }

            if (IsRunning && !inputHandler.GetRunKeyInputHeld())
            {
                IsRunning = false;
            }
            if (IsCrouched && !inputHandler.GetCrouchKeyInputHeld())
            {
                IsCrouched = false;
            }

            HandleMovement();
        }

        void CheckGround()
        {
            IsGround = false;

            if (LastJumpedTime + 0.1 > Time.time)
            {
                return;
            }

            //Feet가 Floor Layer에 닿아있는가??
            if (Physics.CheckSphere(FeetTransform.position, GroundCheckDistanceInAir, FloorMask))
            {
                PlayerAnimator.SetBool(GameConstants.playerJumpNow, false);
                IsGround = true;
                return;
            }

            return;
        }

        void HandleMovement()
        {
            //캐릭터 시야의 가로 회전
            {
                //Alt 버튼을 누르고 있나요?
                if (inputHandler.GetAltInputHeld())
                {
                    //카메라 각도만 변경
                    CameraHorizonalAngle += inputHandler.GetLookInputHorizontal() * RotationSpeed * rotationMultiplier;
                }
                else
                {
                    //유저 회전
                    PlayerBody.MoveRotation(PlayerBody.rotation * Quaternion.Euler(new Vector3(0f, (inputHandler.GetLookInputHorizontal() * RotationSpeed * rotationMultiplier), 0f)));
                }

                //Alt에서 손떼면 시야 정면
                if (inputHandler.GetAltInputUp())
                {
                    CameraHorizonalAngle = 0f;
                }

                //카메라 각도가 너무 돌아가지 않게 제한
                CameraHorizonalAngle = Mathf.Clamp(CameraHorizonalAngle, -179f, 179f);
            }

            //캐릭터 시야의 세로 회전
            {
                CameraVerticalAngle += inputHandler.GetLookInputVertical() * RotationSpeed * rotationMultiplier;
                CameraVerticalAngle = Mathf.Clamp(CameraVerticalAngle, -49f, 79f);
            }

            //실제 카메라 각도 변경
            PlayerCamera.transform.localEulerAngles = new Vector3(CameraVerticalAngle, CameraHorizonalAngle, 0f);

            {

                //땅에 닿아있니?
                if (IsGround)
                {

                    //입력값 받기
                    Vector3 inputVector = inputHandler.GetMoveInput();

                    //실제 벡터 계산
                    Vector3 moveVector = transform.TransformDirection(inputVector) * ModifiedMovementSpeed;

                    int animateZ = 0;
                    if (inputHandler.HasVerticalPlusInput())
                    {
                        animateZ = 1;
                    }
                    else if (inputHandler.HasVerticalMinusInput())
                    {
                        animateZ = -1;
                    }

                    int animateX = 0;
                    if (inputHandler.HasHorizonalPlusInput())
                    {
                        animateX = 1;
                    }
                    else if (inputHandler.HasHorizonalMinusInput())
                    {
                        animateX = -1;
                    }

                    //애니메이션 작동
                    PlayerAnimator.SetFloat(GameConstants.playerVerticalVelocity, animateZ * 0.5f * GravityForAnimator);
                    PlayerAnimator.SetFloat(GameConstants.playerHorizonalVelocity, animateX * 0.5f * GravityForAnimator);


                    //속도 부여하기
                    PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);

                    //걷는 소리가 있다면
                    if (PlayerWalkingAudioSource != null)
                    {
                        //움직이고 있다면 걷는 소리 내기
                        if (moveVector.x != 0 || moveVector.z != 0)
                        {
                            //재생 중이 아니라면 재생
                            if (!PlayerWalkingAudioSource.isPlaying)
                            {
                                PlayerWalkingAudioSource.Play();
                            }
                        }
                        else
                        {
                            PlayerWalkingAudioSource.Stop();
                        }
                    }
                    else
                    {
                        Debug.LogAssertion("플레이어 걷는 소리가 없습니다");
                    }

                    //점프 버튼 누름?
                    if (inputHandler.GetJumpInputDown())
                    {
                        //짬-프
                        PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

                        //점프 애니메이션 ㄱㄱ
                        PlayerAnimator.SetTrigger(GameConstants.playerJumpTrigger);
                        PlayerAnimator.SetBool(GameConstants.playerJumpNow, true);

                        //웅크리기 상태 해제
                        if (IsCrouched)
                        {
                            IsCrouched = false;
                        }

                        //점프 소리?
                        if (PlayerJumpingAudioSource != null)
                        {
                            if (!PlayerJumpingAudioSource.isPlaying)
                            {
                                PlayerJumpingAudioSource.Play();
                            }
                        }
                        else
                        {
                            Debug.LogAssertion("플레이어 점프 소리가 없습니다");
                        }

                        //내가 점프한 시간 기억하기
                        LastJumpedTime = Time.time;
                        HasJumpedThisFrame = true;



                        //IsGround => false
                        IsGround = false;
                    }
                }
                //난 공중에 있서
                else
                {
                    //뭘 해야하나
                }
            }
        }

        //public Vector3 GetDirectionReorientedOnSlope(Vector3 direction, Vector3 slopeNormal)
        //{
        //    Vector3 directionRight = Vector3.Cross(direction, transform.up);
        //    return Vector3.Cross(slopeNormal, directionRight).normalized;
        //}
    }
}
