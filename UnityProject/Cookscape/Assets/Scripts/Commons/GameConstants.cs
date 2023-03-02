
namespace UnityProject.Cookscape
{
    //문자열 입력 틀릴 수도 있으니까 정리해놓기
    public class GameConstants
    {
        //Input.GetAxis() 용
        public const string axisNameVertical = "Vertical";
        public const string axisNameHorizontal = "Horizontal";
        public const string mouseAxisNameVertical = "Mouse Y";
        public const string mouseAxisNameHorizontal = "Mouse X";

        public const string buttonNameJump = "Jump";

        //Animator Check 용
        //앞뒤 키입력값 벡터
        public const string playerVerticalVelocity = "VerticalVelocity";
        //좌우 키입력값 벡터
        public const string playerHorizonalVelocity = "HorizonalVelocity";
        //점프Trigger
        public const string playerJumpNow = "JumpNow";
        //점프함
        public const string playerJumpTrigger = "DoJump";
        //착지함
        public const string playerOnGround = "IsGround";
    }
}
