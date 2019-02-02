import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import "./ProfileComponent.css"
class ProfileComponent extends Component {

    constructor(props){
        super(props);
        this.state = {
            redirect: false
        }
    }

    componentDidMount(){
        
        if(!localStorage.getItem('token')){
            this.setState({
                redirect: true
            })
        }
    }

    render(){
        
        if(this.state.redirect) {
            return <Redirect to='/login' />
        }
        return <div className="login-pages">
            <form
                onSubmit={event => this.registerUser(event)}
                ref={form => this.loginForm = form}
                className="form"
            >
                <h2>Profil</h2>
                <input 
                    type="text"
                    placeholder="Ola"
                    ref={input => this.nameInput = input}
                    value={localStorage.getItem('name')}
                />
                <input
                    type="text"
                    placeholder="E-post"
                    ref={input => this.emailInput = input}
                    value={localStorage.getItem('email')}
                />
                <input 
                    type="text"
                    placeholder="Passord"
                    ref={input => this.passwordInput = input}
                />
                <button disabled>Oppdater info</button>
                
            </form>
        </div>
    }
}

export default ProfileComponent;