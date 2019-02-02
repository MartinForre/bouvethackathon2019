import React, { Component } from 'react';
import './Home.css';

class HomeComponent extends Component {

    constructor(props){
        super(props);

        this.state = {
            uid: localStorage.getItem('uid'),
            balance: 0,
            totalWeight: 0,
            details: []
        }
    }

    componentDidMount() {
        this.getStatistics();
    }

    getStatistics() {
        if(!this.isLoggedIn())
            return;

        fetch(`https://bouvet-panther-api.azurewebsites.net/api/user/balance?userId=${this.state.uid}`)
            .then(response => response.json())
            .then(response => {
                this.setState({balance: response.balance, details: response.details}, this.calculateTotalPickedWeight())
            })
            .catch(error => console.log(error))
    }

    isLoggedIn() {
        return localStorage.getItem('uid') != null;
    }

    calculateTotalPickedWeight() {
        let initalWeightValue = 0;
        let totalWeight = this.state.details.reduce(
            (accumulator, currentValue) => accumulator + currentValue.weight,
            initalWeightValue
        )
        this.setState({totalWeight: totalWeight});
    }

    render(){
        if (!this.isLoggedIn()) {
            return (
                <div>
                    <p>
                        Du er ikke logget inn
                    </p>
                </div>
            );
        }
        return (
            <div>
                <div className="balance">
                    Din plukkesaldo: <strong>{this.state.balance}</strong> kroner
                </div>

                <p className="totalWeight">
                    Takk for at du har plukket {this.state.totalWeight} kg plast i år
                </p>
            </div>
        )
    }
}

export default HomeComponent;