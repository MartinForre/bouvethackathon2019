import React, { Component } from 'react';
import { FaCheck, FaBan } from 'react-icons/fa';
import './VerifiedQrComponent.css';

class VerifiedQrComponent extends Component {


 render(){
     switch(this.props.validationResponse){
        case 'OK':
            return <div><div class="icon-container"> <FaCheck className="green huge-icon"/></div><div id="response-text"> Kanon, du klarte å registrere en pose. </div></div>
        case 'InUse':
            return <div><div class="icon-container"> <FaCheck className="green huge-icon"/> </div><div id="response-text">Saken er.. Du har allerede registrert denne.. Du er flink!</div> </div>
        case 'Unknown':
        default:
            return <div><div class="icon-container"> <FaBan className="red huge-icon"/> </div><div id="response-text">Beklager, denne er dessverre i bruk... </div></div>

     }
 }
}

export default VerifiedQrComponent;