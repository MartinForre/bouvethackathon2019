import React, { Component } from 'react';
import './Home.css';

class HomeComponent extends Component {

    constructor(props){
        super(props);

        this.state = {

        }
    }

    render(){

        return (
            <div>
                <h2>Home</h2>

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