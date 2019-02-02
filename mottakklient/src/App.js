import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './App.css';
import Routes from './containers/navigation';

class App extends Component {
  render() {
    return (
      <div className="App">
          <Routes/>
      </div>
    );
  }
}

export default App;
