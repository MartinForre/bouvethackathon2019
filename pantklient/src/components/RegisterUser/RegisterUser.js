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
            firstname: this.firstname.value,
            lastname: this.lastname.value,
            email: this.email.value
        }

        const options = {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(userData),
            headers: {
                'Content-Type': 'application/json'
            },
        };

        //fetch('https://bouvet-panther-api.azurewebsites.net/api/user/register', options)
        //    .then(res => res.json())
        //    .then(res => this.handleRespone(res))
        //    .catch(error => console.log(error));
        console.log(userData);
        this.setState({redirect: true})
    }

    handleRegisterResponse(response) {

    }

    render(){
        if(this.state.redirect) {
            return <Redirect to='/login' />
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
                                placeholder="Fornavn"
                                ref={input => this.firstname = input}
                            />
                            <input 
                                type="text"
                                placeholder="Etternavn"
                                ref={input => this.lastname = input}
                            />
                            <input
                                type="email"
                                placeholder="E-post"
                                ref={input => this.email = input}
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