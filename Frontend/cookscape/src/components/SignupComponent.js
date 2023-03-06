import { useRef, useState } from "react";
import { axiostInstance } from "../utils/getAxios";

const className_inputContainer = "inputContainer";

const SignupComponent = ({togglePage}) => {
  const [userid, setUserid] = useState("");
  const [password, setPassword] = useState("");
  const [passwordDuplicate, setPasswordDuplicate] = useState("");
  const [nickname, setNickname] = useState("");

  const [loginDuplicate , setLoginDuplicate] = useState(0);
  const [nicknameDupplicate , setNicknameDuplicate] = useState(0);
  const [passwordCheck , setPasswordCheck] = useState(0);

  const checkIdDuplicate = (e) => {
    e.preventDefault()
    axiostInstance.get(`user/id-check/${userid}`).then(res => {
      console.log(res);
      if( res.data.msg === "OK") {
        setLoginDuplicate(1);
      }
    }).catch(err => {
      setLoginDuplicate(-1);
      console.log(err);
    })
  }

  const checkNicknameDuplicate = (e) => {
    e.preventDefault()
    axiostInstance.get(`user/nickname-check/${nickname}`).then(res => {
      console.log(res);
      setNicknameDuplicate(1)
    }).catch(err => {
      console.log(err);
      setNicknameDuplicate(-1)
    })
  }

  const checkPassword = (e) => {
    setPasswordDuplicate(e.target.value);

    if ( password === e.target.value ) {
      setPasswordCheck(1);
    }else {
      setPasswordCheck(-1);
    }
  }

  const doSignup = (e) => {
    e.preventDefault()

    if( loginDuplicate !== 1 ){
      alert("아이디 중복체크를 해주세요!")
      return;
    }

    if( nicknameDupplicate !== 1 ){
      alert("닉네임 중복체크를 해주세요!")
      return;
    }

    if( passwordCheck !== 1 ){
      alert("비밀번호가 같지 않습니다!")
      return;
    }

    const signUpDto = {
      loginId : userid,
      nickname: nickname,
      password: password,
    }

    axiostInstance.post("user/signup", signUpDto).then(res => {
      console.log("성공");
      togglePage();
    }).catch(err => {
      console.log(err);
    });
  }

  return (
    <div>
      <h1>회원가입</h1>
      <form onSubmit={doSignup}>
        <div className={className_inputContainer}>
          <label>
            아이디
          </label>
          <input type={"text"} value={userid} onChange={(e) => { setUserid(e.target.value); setLoginDuplicate(0); }} />
          <button onClick={checkIdDuplicate}>중복확인</button>
        </div>
        <div>
        { 
          loginDuplicate === 1 && ( <div>사용 가능한 아이디입니다!</div> )
        }
        {
          loginDuplicate === -1 && ( <div>사용할 수 없는 아이디입니다!</div> )
        }
        </div>
        <div className={className_inputContainer}>
          <label>
            비밀번호
          </label>
          <input type={"password"} value={password} onChange={(e) => { setPassword(e.target.value) }} />
        </div>
        <div className={className_inputContainer}>
          <label>
            비밀번호 재입력
          </label>
          <input type={"password"} value={passwordDuplicate} onChange={checkPassword} />
        </div>
        <div>
        { 
          passwordCheck === 1 && ( <div>비밀번호가 일치합니다!</div> )
        }
        {
          passwordCheck === -1 && ( <div>비밀번호가 일치하지 않습니다</div> )
        }
        </div>
        <div className={className_inputContainer}>
          <label>
            닉네임
          </label>
          <input type={"text"} value={nickname} onChange={(e) => { setNickname(e.target.value); setNicknameDuplicate(0); }} />
          <button onClick={checkNicknameDuplicate} >중복확인</button>
        </div>
        <div>
        { 
          loginDuplicate === 1 && ( <div>사용 가능한 닉네임입니다!</div> )
        }
        {
          loginDuplicate === -1 && ( <div>사용할 수 없는 닉네임입니다!</div> )
        }
        </div>
        <div className={className_inputContainer}>
          <button type={"submit"}>
            회원가입
          </button>
        </div>
      </form>
    </div>
  )
}

export default SignupComponent;