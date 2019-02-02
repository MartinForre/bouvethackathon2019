import React, { Component } from 'react';
import './Home.css';

class HomeComponent extends Component {

    constructor(props){
        super(props);

        this.state = {
            uid: localStorage.getItem('uid'),
            balance: 0
        }
    }

    componentDidMount() {
        this.getBalance();
    }

    getBalance() {
        if(!localStorage.getItem('uid'))
            return;

        fetch(`https://bouvet-panther-api.azurewebsites.net/api/user/balance?${this.state.uid}`)
            .then(response => response.json())
            .then(response => this.setState({balance: response.balance}))
            .catch(error => console.log(error))
    }

    render(){

        return (
            <div>
                <h2>Home</h2>
                <div> Din plukkesaldo: <strong>{this.getBalance()}</strong> kroner </div>


                <p>
                    Din bydel har plukket 12 345 kg plast i år
                </p>

                <p>
                    Takk for at du har plukket {this.state.balance} kg plast i år
                    </p>
                <p>
                    Din bydel har plukket 12 345 kg plast i år
                </p>

                <p>
                    Takk for at du har plukket 5,3 kg plast i år
                </p>
            </div>
        )
    }
}

export default HomeComponent;