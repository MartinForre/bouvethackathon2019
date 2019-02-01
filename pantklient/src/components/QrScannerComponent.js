import React, { Component } from "react";
import QrReader from "react-qr-reader";

class QrScannerComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            delay: 300,
            result: "No result"
        };
        this.handleScan = this.handleScan.bind(this);
    }
    handleScan(data) {
        if (data) {
            this.setState({
                result: data
            });

            // TODO validere at det er en gyldig URL til godkjent domene etc.

            document.location.href = data;
        }
    }
    handleError(err) {
        console.error(err);
    }
    render() {
        return (
            <div>
                <p>{this.state.result}</p>
                <QrReader
                    delay={this.state.delay}
                    onError={this.handleError}
                    onScan={this.handleScan}
                    style={{ width: "50%" }}
                />

            </div>
        );
    }
}

export default QrScannerComponent;