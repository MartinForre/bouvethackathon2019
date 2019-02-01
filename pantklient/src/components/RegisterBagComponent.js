import React, { Component } from 'react';
import { FaRecycle } from 'react-icons/fa';

class RegisterBagComponent extends Component {

    constructor(props){
        super(props);

        this.state = {
            bagId: null,
            isBagIdValidated: false,
            validationResponse: '',
            uid: null
        }
    }

    componentDidMount() {
        const  { id } = this.props.match.params;
        this.checkOrGetUserId();
        this.setState({bagId:id});
    }

    checkOrGetUserId() {
        let id = localStorage.getItem('uid');
        if(id == null){
            //TODO fetch user ID from API and store to localstorage.
            //mock
            id = 123141231
        }

        localStorage.setItem('uid', id);
        this.setState({uid: id});
    }

    verifyBagId() {
   
        const myPost = {
            qrCode: this.state.bagId,
            userid: this.state.uid
          }
          
          const options = {
            method: 'POST',
            body: JSON.stringify(myPost),
            headers: {
              'Content-Type': 'application/json'
            }
          };
          
          fetch('https://jsonplaceholder.typicode.com/posts', options)
            .then(res => res.json())
            .then(res => console.log(res));



    }

    handleRespone(response){
        console.log((response))
        //TODO gjør noe med responsen her!
        // vise godkjent / ikke godkjent, bla bla
    }

    render(){

        this.verifyBagId();


        return(
            <div className="App">
                <h1>REGISTER BAG</h1>
                <p>Din pose har id: {this.state.bagId}</p>

                {this.state.isBagIdValidated ? <p>Bag response: {this.state.validationResponse} </p> : <FaRecycle className="App-logo"/>}
            </div>
        )
    }
}

export default RegisterBagComponent;