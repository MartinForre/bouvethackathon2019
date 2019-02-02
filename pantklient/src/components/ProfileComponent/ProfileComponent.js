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

    registerUser(event) {
        event.preventDefault();
        let userData = {
            name: this.nameInput.value,
            password: this.passwordInput.value,
            email: this.emailInput.value,
            userId: localStorage.getItem('uid')
        }

        const options = {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(userData),
            headers: {
                'Content-Type': 'application/json',
                'Authorization' : localStorage.getItem('token')
            },
        };

        fetch('https://bouvet-panther-api.azurewebsites.net/api/user/update', options)
            .then(res => res.json())
            .then(res => this.handleUpdateResponse(res))
            .catch(error => console.log(error));
        console.log(userData);
        this.setState({redirect: true})
    }

    handleUpdateResponse(response) {
        localStorage.setItem('uid', response.userId)
        localStorage.setItem('name', response.name)
        localStorage.setItem('email', response.email)
        localStorage.setItem('token', response.token)
    }
    
    render(){
        
        if(this.state.redirect) {
            return <Redirect to='/login' />
        }
        return <div className="login-pages">
            <form
                onSubmit={event => this.updateUser(event)}
                ref={form => this.loginForm = form}
                className="form">
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
                <button>Oppdater info</button>
                
            </form>
        </div>
    }
}

export default ProfileComponent;