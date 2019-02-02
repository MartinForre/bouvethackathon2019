import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import "./ProfileComponent.css"
class ProfileComponent extends Component {

    constructor(props){
        super(props);
        this.state = {
            redirect: false,
            email:null,
            name:null,
        }
    }

    componentDidMount(){
        
        if(!localStorage.getItem('token')){
            this.setState({
                redirect: true
            })
        }else{
            this.setState({
                email:localStorage.getItem('email'),
                name:localStorage.getItem('name')
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
    }

    handleUpdateResponse(response) {
        localStorage.setItem('uid', response.userId)
        localStorage.setItem('name', response.name)
        localStorage.setItem('email', response.email)
        localStorage.setItem('token', response.token)
        this.setState({
            email:localStorage.getItem('email'),
            name:localStorage.getItem('name')
        })
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
                    value={this.state.name}
                />
                <input
                    type="text"
                    placeholder="E-post"
                    ref={input => this.emailInput = input}
                    value={this.state.email}
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