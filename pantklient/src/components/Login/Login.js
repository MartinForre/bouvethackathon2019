import React, { Component } from "react";
import { Redirect } from "react-router-dom";


import "./Login.css";

class Login extends Component {
  //TODO: Put firebase login stuff in FirebaseService
  
  constructor(props) {
    super(props);
    this.authWithFacebook = this.authWithFacebook.bind(this);
    this.authWithEmailPassword = this.authWithEmailPassword.bind(this);
    this.state = {
      redirect: false
    };
  }

  authWithFacebook() {
    
  }

  authWithEmailPassword(event) {
   
  }

  render() {
    if (this.state.redirect === true) {
      return <Redirect to="/"/>;
    }
    return (
      <div className="loginContainer">
       

        <button
          style={{ width: "100%" }}
          className="bp3-button bp3-intent-primary"
          onClick={() => {
            this.authWithFacebook();
          }}
        >
          Log in with Facebook
        </button>
        <hr style={{ marginTop: "10px", marginBottom: "10px" }} />
        <form
          onSubmit={event => {
            this.authWithEmailPassword(event);
          }}
          ref={form => {
            this.loginForm = form;
          }}
        >
          <div
            style={{ marginBottom: "10px" }}
            className="bp3-callout bp3-icon-info-sign"
          >
            <h2>Note</h2>
            If you don't have an account already, this form will create your
            account.
          </div>
          <label className="bp3-label">
            Email
            <input
              style={{ width: "100%" }}
              className="bp3-input"
              name="email"
              type="email"
              ref={input => {
                this.emailInput = input;
              }}
              placeholder="Email"
            />
          </label>
          <label className="bp3-label">
            Password
            <input
              style={{ width: "100%" }}
              className="bp3-input"
              name="password"
              type="password"
              ref={input => {
                this.passwordInput = input;
              }}
              placeholder="Password"
            />
          </label>
          <input
            style={{ width: "100%" }}
            type="submit"
            className="bp3-button bp3-intent-primary"
            value="Log In"
          />
        </form>
      </div>
    );
  }
}
export default Login;