import React, { Component } from "react";
import QrReader from "react-qr-reader";
import './QrScannerComponent.css';
import { Redirect } from "react-router-dom";

class QrScannerComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            delay: 300,
            result: ""
        };
        this.handleScan = this.handleScan.bind(this);
    }
    handleScan(data) {
        if (data) {

            //get code out of data
            data = data.match(/\/([^\/]+)\/?$/)[1];
            var url = `/control/${data}`;
            this.props.history.push(url);

        }
    }
    handleError(err) {
        console.error(err);
    }
    render() {
        return (
            <div id="qr-code-page">
                <div id="qr-container">
                    <QrReader
                        delay={this.state.delay}
                        onError={this.handleError}
                        onScan={this.handleScan}
                        className="full-width" 
                    />
                </div>
            </div>
        );
    }
}

export default QrScannerComponent;