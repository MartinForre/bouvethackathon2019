import React, { Component } from 'react';

class BagControlComponent extends Component {

    constructor(props){
        super(props);

        this.state = {
            bagId: null,
            showButtons: false
        }
    }

    componentDidMount() {
        const  { id } = this.props.match.params;
        this.setState({ bagId : id });
        this.verifyBagId(id);
    }

    verifyBagId(id) {
        const myPost = {
            "bagId": id,
            "location": "Oslo",
            "value": Math.random()*1000,
            "weight": Math.random()*1000
        };
          
        const options = {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(myPost),
            headers: {
                'Content-Type': 'application/json'
            },
        };

        fetch('https://bouvet-panther-api.azurewebsites.net/api/Receive/Receive', options)
            .then(res => res.json())
            .then(res => this.handleRespone(res))
            .catch(error => console.log(error));
    }

    handleRespone(response){
        console.log(response);
        this.setState({
            showButtons: true
        });
    }

    render(){

        return(
            <div className="App">
               <p>Pakke ID: {this.state.bagId}</p>

                {this.state.showButtons ?
                    <div>
                        <button>Godkjent</button>
                        <button>Ikke godkjent</button>
                    </div> : ''
                }


            </div>
        )
    }
}

export default BagControlComponent;