import React, { Component } from 'react';
import { Link } from 'react-router-dom';
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
            const options = {
                headers: {
                    'Authorization' : localStorage.getItem('token')
                },
            };
        fetch(`https://bouvet-panther-api.azurewebsites.net/api/user/balance?userId=${this.state.uid}`, options)
            .then(response => response.json())
            .then(response => {
                this.setState({balance: response.balance, details: response.details}, this.calculateTotalPickedWeight)
            })
            .catch(error => console.log(error))
    }

    isLoggedIn() {
        return localStorage.getItem('uid') != null;
    }

    
    calculateTotalPickedWeight = () => {
        let initalWeightValue = 0;
        let totalWeight = this.state.details.reduce(
            (accumulator, currentValue) => accumulator + currentValue.weight,
            initalWeightValue
        )
        this.setState({totalWeight: totalWeight});
    }

    render(){
        return (
            <div>
                <div className="balance">
                <div>Din plukkesaldo: <strong>{this.state.balance}</strong> kroner</div>
                <div>
                    <Link to="/"><button>Cash ut!</button></Link>
                </div>
                
                </div>
                <p className="totalWeight">
                    Takk for at du har plukket {this.state.totalWeight} kg plast i år
                </p>
                <p className="totalArea">
                    Din bydel har plukket{this.state.totalWeight} kg plast i år
                </p>
            </div>
        )
    }
}

export default HomeComponent;