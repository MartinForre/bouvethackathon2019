import React, { Component } from 'react';
import { FaRecycle } from 'react-icons/fa';
import VerifiedQrComponent from '../VerifiedQrComponent/VerifiedQrComponent';

class RegisterBagComponent extends Component {

    constructor(props){
        super(props);

        this.state = {
            bagId: null,
            validationResponse: null,
            uid: null
        }
    }

    componentDidMount() {
        const  { id } = this.props.match.params;
        this.setState({ bagId : id }, () =>{
            this.verifyBagId();
        });
        //this.checkOrGetUserId();
    }


    verifyBagId() {
        const myPost = {
            BagId: this.state.bagId,
            UserId: this.state.uid   
        }
        const options = {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(myPost),
            headers: {
                'Content-Type': 'application/json',
                'Authorization' : localStorage.getItem('token')
            },
        };

        fetch('https://bouvet-panther-api.azurewebsites.net/api/QR/Activate', options)
            .then(res => {
                console.log(res)
                localStorage.setItem('token', res.headers.get('x-plukk-token')); //TODO doesnt work
                return res.json();
            })
            .then(res => this.handleRespone(res))
            .catch(error => console.log(error));
    }

    handleRespone(response){
        localStorage.setItem('uid', response.userId);
        this.setState({
            uid: response.userId,
            validationResponse: response.status
        });
    }

    render(){

        if(this.state.uid == null){
            return <FaRecycle className="App-logo"/>
        }

        return(
            <div className="App">
                {this.state.validationResponse ?
                    <VerifiedQrComponent
                        validationResponse= {this.state.validationResponse}/>
                :
                    <FaRecycle className="App-logo"/>}
            </div>
        )
    }
}

export default RegisterBagComponent;