import React, { Component } from "react";
import QrReader from "react-qr-reader";

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
                <div>{this.state.result}</div>
                <div id="qr-container">
                    <QrReader
                        delay={this.state.delay}
                        onError={this.handleError}
                        onScan={this.handleScan}
                        style={{ width: "100%" }} //TODO seriøst fiks denne. ser stygg ut
                    />
                </div>

            </div>
        );
    }
}

export default QrScannerComponent;