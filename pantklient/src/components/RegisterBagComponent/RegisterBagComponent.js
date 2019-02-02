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
        this.setState({ bagId : id });
        this.checkOrGetUserId();
    }

    checkOrGetUserId() {
        let id = localStorage.getItem('uid');

        if(id == null){
            fetch('https://bouvet-panther-api.azurewebsites.net/api/User/Register', {
                method: "GET",
            }).then(response => response.json())
            .then(response => {
                localStorage.setItem('uid', response.uid)
                this.setState(
                    {uid: response.uid},
                    () => this.verifyBagId()
                )
            })
            .catch(error => console.log(error)) //TODO handle error riktig.
        } else {
            this.setState(
                {uid: id},
                () => this.verifyBagId()
            )
        }
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
                'Content-Type': 'application/json'
            },
        };

        fetch('https://bouvet-panther-api.azurewebsites.net/api/QR/Activate', options)
            .then(res => res.json())
            .then(res => this.handleRespone(res))
            .catch(error => console.log(error));
    }

    handleRespone(response){
        this.setState({
            validationResponse: response.status
        });
    }

    render(){

        if(this.state.uid == null){
            return (<div>
                <FaRecycle/>
            </div>);
        }

        return(
            <div className="App">
                <h1>REGISTER BAG</h1>
                <p>Din pose har id: {this.state.bagId}</p>

                {this.state.validationResponse ? 
                <VerifiedQrComponent
                   validationResponse= {this.state.validationResponse}/> : 
                <FaRecycle className="App-logo"/>}
            </div>
        )
    }
}

export default RegisterBagComponent;