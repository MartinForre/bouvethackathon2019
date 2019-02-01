import React, { Component } from 'react';
import { FaRecycle } from 'react-icons/fa';
import VerifiedQrComponent from './VerifiedQrComponent';
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
        this.verifyBagId();
    }

    checkOrGetUserId() {
        let id = localStorage.getItem('uid');

        //hack
        id=12312312;
        if(id == null){
            fetch('http://bouvet-panther-api.azurewebsites.net/api/User/Register', {
                method: "GET",
                mode: "no-cors"
            }).then(response => response.text())
                .then(response => {
                    console.log(response)
                    this.setState({uid: response.uid})
                })
                .catch(error => console.log(error)) //TODO handle error riktig.
        }else{
            this.setState({uid: id})
        }
    }

    verifyBagId() {
   
        const myPost = {
            BagId: 123,
            UserId: 323
          }
          
          const options = {
            method: 'POST',
            mode:"cors",
            body: JSON.stringify(myPost),
            headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json'
            },
          };
          
  
          fetch('https://bouvet-panther-api.azurewebsites.net/api/QR/Activate', options)
            .then(res => res.json())
            .then(res => this.handleRespone(res))
            .then(res => console.log(res));
            
/*


        //this.handleRespone({status: 200, validationResponse: 'verified'});
        fetch('http://bouvet-panther-api.azurewebsites.net/api/QR/Activate?BagId=' + this.state.bagId + '&UserId=' + this.state.uid, {
            method: "POST",
            mode: "no-cors"
        }).then(response => response)
            .then(response => this.handleRespone(response))
            .catch(error => console.log(error)) //TODO handle error riktig.

            https://jsonplaceholder.typicode.com/posts
            */
    }

    handleRespone(response){
        //TODO gjør noe med responsen her!
        // vise godkjent / ikke godkjent, bla bla
        console.log(response);
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