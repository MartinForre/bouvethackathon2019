import React, { Component } from 'react';
import { FaFacebook, FaTwitter } from 'react-icons/fa';
import './SoMeComponent.css'

class SoMeComponent extends Component {
    render(){

        return(
            <div className="SoMe">
                    <FaFacebook className="Facebook"/>
                    <FaTwitter className="Twitter"/>
            </div>
        )
    }
}

export default SoMeComponent;