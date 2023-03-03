using UnityEngine;

namespace UnityProject.Cookscape
{
    public class PlayerCommonController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("�÷��̾��� ����ī�޶�")]
        public Transform PlayerCamera;

        [Tooltip("�ȴ� �Ҹ�")]
        public AudioSource PlayerWalkingAudioSource;

        [Tooltip("�޸��� �Ҹ�")]
        public AudioSource PlayerRunningAudioSource;

        [Tooltip("���� �Ҹ�")]
        public AudioSource PlayerJumpingAudioSource;

        [Tooltip("�÷��̾��� rigidbody")]
        public Rigidbody PlayerBody;

        [Tooltip("�� üũ")]
        public Transform FeetTransform;

        [Tooltip("Check FloorLayer")]
        public LayerMask FloorMask;

        Animator PlayerAnimator;
        InputHandler inputHandler;
        //CharacterController characterController;


        //�� ���� ����ִ�?
        public bool IsGround { get; private set; }
        //���� �����ߴ�
        public bool HasJumpedThisFrame { get; private set; }
        //���� ����ִٰ� �����ϴ� �Ÿ�
        [Tooltip("distance from the bottom of the character controller capsule to test for grounded")]
        public float GroundCheckDistance = 0.05f;
        //�ʱ� ī�޶� ���� ����
        public float CameraVerticalAngle = 20f;
        //�ʱ� ī�޶� ���� ����
        public float CameraHorizonalAngle = 0f;
        //�ִ� �ӵ�
        public float MaxSpeedOnGround = 10f;
        //ĳ���� �ӵ�
        public Vector3 CharacterVelocity { get; set; }

        Vector3 GroundNormal;
        //���������� ������ �ð�
        float LastJumpedTime = -10;

        bool IsRunning = false;
        bool IsCrouched = false;

        //�ϴÿ� ���� �� ���� ����ִٰ� �����ϴ� �Ÿ�
        const float GroundCheckDistanceInAir = 0.07f;
        //�ּ��� �ϴÿ� �� �־�� �ϴ� �ð�
        const float JumpGroundingPreventionTime = 0.2f;
        //ȸ�� �ӵ�
        [SerializeField] float RotationSpeed = 20f;
        //���� �ӵ�
        [SerializeField] float JumpForce = 9f;
        //�̵� �ӵ�
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
                Debug.Log("�� ���� �ʹ� ����");
            }

            HasJumpedThisFrame = false;

            bool wasGrounded = IsGround;
            CheckGround();
            //PlayerAnimator.SetBool(GameConstants.playerOnGround, IsGround);

            //���� �� �ε��� ����
            if (IsGround && !wasGrounded)
            {
                PlayerAnimator.SetBool(GameConstants.playerJumpNow, false);
                Debug.Log("����!");
            }

            //�޸���&�ɱ⸦ ����ٸ�?
            if (inputHandler.GetRunKeyInputDown())
            {
                IsRunning = true;
                IsCrouched = false;
                Debug.Log("�޷�!");
            }
            else if (inputHandler.GetCrouchKeyInputDown())
            {
                IsRunning = false;
                IsCrouched = true;
                Debug.Log("��ũ��");
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

            //Feet�� Floor Layer�� ����ִ°�??
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
            //ĳ���� �þ��� ���� ȸ��
            {
                //Alt ��ư�� ������ �ֳ���?
                if (inputHandler.GetAltInputHeld())
                {
                    //ī�޶� ������ ����
                    CameraHorizonalAngle += inputHandler.GetLookInputHorizontal() * RotationSpeed * rotationMultiplier;
                }
                else
                {
                    //���� ȸ��
                    PlayerBody.MoveRotation(PlayerBody.rotation * Quaternion.Euler(new Vector3(0f, (inputHandler.GetLookInputHorizontal() * RotationSpeed * rotationMultiplier), 0f)));
                }

                //Alt���� �ն��� �þ� ����
                if (inputHandler.GetAltInputUp())
                {
                    CameraHorizonalAngle = 0f;
                }

                //ī�޶� ������ �ʹ� ���ư��� �ʰ� ����
                CameraHorizonalAngle = Mathf.Clamp(CameraHorizonalAngle, -179f, 179f);
            }

            //ĳ���� �þ��� ���� ȸ��
            {
                CameraVerticalAngle += inputHandler.GetLookInputVertical() * RotationSpeed * rotationMultiplier;
                CameraVerticalAngle = Mathf.Clamp(CameraVerticalAngle, -49f, 79f);
            }

            //���� ī�޶� ���� ����
            PlayerCamera.transform.localEulerAngles = new Vector3(CameraVerticalAngle, CameraHorizonalAngle, 0f);

            {

                //���� ����ִ�?
                if (IsGround)
                {

                    //�Է°� �ޱ�
                    Vector3 inputVector = inputHandler.GetMoveInput();

                    //���� ���� ���
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

                    //�ִϸ��̼� �۵�
                    PlayerAnimator.SetFloat(GameConstants.playerVerticalVelocity, animateZ * 0.5f * GravityForAnimator);
                    PlayerAnimator.SetFloat(GameConstants.playerHorizonalVelocity, animateX * 0.5f * GravityForAnimator);


                    //�ӵ� �ο��ϱ�
                    PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);

                    //�ȴ� �Ҹ��� �ִٸ�
                    if (PlayerWalkingAudioSource != null)
                    {
                        //�����̰� �ִٸ� �ȴ� �Ҹ� ����
                        if (moveVector.x != 0 || moveVector.z != 0)
                        {
                            //��� ���� �ƴ϶�� ���
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
                        Debug.Log("do not have walk sound");
                    }

                    //���� ��ư ����?
                    if (inputHandler.GetJumpInputDown())
                    {
                        //«-��
                        PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

                        //���� �ִϸ��̼� ����
                        PlayerAnimator.SetTrigger(GameConstants.playerJumpTrigger);
                        PlayerAnimator.SetBool(GameConstants.playerJumpNow, true);

                        //��ũ���� ���� ����
                        if (IsCrouched)
                        {
                            IsCrouched = false;
                        }

                        //���� �Ҹ�?
                        if (PlayerJumpingAudioSource != null)
                        {
                            if (!PlayerJumpingAudioSource.isPlaying)
                            {
                                PlayerJumpingAudioSource.Play();
                            }
                        }
                        else
                        {
                            Debug.Log("�÷��̾� ���� �Ҹ��� �����ϴ�");
                        }

                        //���� ������ �ð� ����ϱ�
                        LastJumpedTime = Time.time;
                        HasJumpedThisFrame = true;



                        //IsGround => false
                        IsGround = false;
                    }
                }
                //�� ���߿� �ּ�
                else
                {
                    //�� �ؾ��ϳ�
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
