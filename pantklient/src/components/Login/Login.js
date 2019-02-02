import React, { Component } from "react";
import { Redirect } from "react-router-dom";

import "./Login.css";

class Login extends Component {
  
  constructor(props) {
    super(props);
    this.authWithUsernamePassword = this.authWithUsernamePassword.bind(this);
    this.state = {
      redirect: false
    };
  }

  authWithUsernamePassword(event) {
    event.preventDefault();
    const username = this.usernameInput.value;
    const password = this.passwordInput.value;
    console.log("Username: " + username)
    console.log("Password: " + password)
    //This would be where a api call would be nice
    if(username === 'SUPERHACKATHON'){
      this.setState({ redirect: true });
    }
  }

  render() {
    if (this.state.redirect === true) {
      return <Redirect to="/"/>;
    }
    return (
    <div className="login-page">
      <div className="form">

        <form className="login-form"
          onSubmit={event => this.authWithUsernamePassword(event)}
          ref={form => this.loginForm = form}>
          
          <input className="text" 
            placeholder="Brukernavn"
            ref={input => this.usernameInput = input}
          />
          <input className="password" 
            placeholder="Passord"
            ref={input => this.passwordInput = input}
          />
          <button>logg inn</button>
          <p className  ="message">Ikke registrert? <a href="#/registerUser">Opprett konto</a></p>
        </form>
      </div>
    </div>
    );
  }
}
export default Login;