import React, { Component } from 'react';

class RegisterBagComponent extends Component {

    constructor(props){
        super(props);

        this.state = {
            bagId: null,
            isBagIdValidated: false,
            validationResponse: ''
        }
    }

    componentDidMount() {
        const  { id } = this.props.match.params;
        this.setState({bagId:id});

        this.verifyBagId();
    }

    verifyBagId() {
        //TODO koble til ekte API, mock for nå
        var that = this;
        setTimeout(function(){
            that.setState({isBagIdValidated: true, validationResponse: "Godkjent"})
        }, Math.random()*1000 * 5);
    }


    render(){



        return(
            <div>
                <h1>REGISTER BAG</h1>
                <p>Din pose har id: {this.state.bagId}</p>

                {this.state.isBagIdValidated ? <p>Bag response: {this.state.validationResponse} </p> : <p>**PROGRESS SPINNER**</p>}
            </div>
        )
    }


}

export default RegisterBagComponent;