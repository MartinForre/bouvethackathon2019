import React, { Component } from 'react';
import { FaCheck, FaBan } from 'react-icons/fa';

class VerifiedQrComponent extends Component {

    constructor(props){
        super(props);
    }

 render(){
     switch(this.props.validationResponse){
        case 'OK':
        return <p> <FaCheck className="green huge-icon"/><br/> Kanon, du klarte å registrere en pose. </p>
        case 'InUse':
        return <div> <FaCheck className="green huge-icon"/><div>Saken er.. Du har allerede registrert denne.. Du er flink!</div>  </div>
        case 'Unknown':
        default:
        return <p> <FaBan className="red huge-icon"/> <br/>Beklager, denne er dessverre i bruk... </p>

     }
 }
}

export default VerifiedQrComponent;