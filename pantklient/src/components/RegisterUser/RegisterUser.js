import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import './RegisterUser.css';

class RegisterComponent extends Component {

    constructor(props){
        super(props);

        this.state = {
            firstname: '',
            lastname: '',
            redirect: false
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
                'Content-Type': 'application/json'
            },
        };

        fetch('https://bouvet-panther-api.azurewebsites.net/api/user/register', options)
            .then(res => res.json())
            .then(res => this.handleRegisterResponse(res))
            .catch(error => console.log(error));
        console.log(userData);
        this.setState({redirect: true})
    }

    handleRegisterResponse(response) {
        localStorage.setItem('uid', response.userId)
        localStorage.setItem('name', response.name)
        localStorage.setItem('email', response.email)
        localStorage.setItem('token', response.token)
    }

    render(){
        if(this.state.redirect) {
            return <Redirect to='/' />
        }
        return (
            <div>
                <div className="login-page">
                    <div className="form">
                        <form
                            onSubmit={event => this.registerUser(event)}
                            ref={form => this.loginForm = form}
                        >
                            <h2>Registrer bruker</h2>
                            <input 
                                type="text"
                                placeholder="Ola"
                                ref={input => this.nameInput = input}
                            />
                            <input
                                type="email"
                                placeholder="E-post"
                                ref={input => this.emailInput = input}
                            />
                            <input 
                                type="text"
                                placeholder="Passord"
                                ref={input => this.passwordInput = input}
                            />
                            <button>Opprett bruker</button>
                            <p className="message">Har du allerede en bruker? <a href="#/login">Logg inn her</a></p>
                        </form>
                    </div>
                </div>
            </div>
        );
    }
}

export default RegisterComponent;