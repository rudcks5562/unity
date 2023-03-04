import {useState} from 'react';
import './App.css';
import MainContent from './components/MainContent';
import SignupComponent from './components/SignupComponent';

const App = () => {
  const [doSignup, setDoSignup] = useState(false);
  const toggleSignupPage = () => {
    setDoSignup(!doSignup);
  }

  return (
    <div className="App">
      <header className="App-header">
        <h1>안녕 나는 헤더</h1>
        <div className={`div-content`}>
          <button onClick={() => {setDoSignup(true)}}>회원가입</button>
        </div>
      </header>
      <section>
        { doSignup ? 
          <SignupComponent togglePage={toggleSignupPage} /> :
          <MainContent />
        }
      </section>
      <footer>
        <h1>안녕 나는 푸터</h1>
      </footer>
    </div>
  );
}

export default App;
