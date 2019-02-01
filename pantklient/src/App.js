import React, { Component } from 'react';
import { HashRouter as Router, Route, Link, Redirect } from 'react-router-dom'
import './App.css';
import HomeComponent from './components/HomeComponent';
import AboutComponent from './components/AboutComponent';
import RegisterBagComponent from './components/RegisterBagComponent';
import QrScannerComponent from './components/QrScannerComponent';

class App extends Component {
  render() {
    return (
        <Router>
            <div>
                <ul>
                    <li>
                        <Link to="/"> Home </Link>
                    </li>
                    <li>
                        <Link to="/scan"> Scanner </Link>
                    </li>
                    <li>
                        <Link to="/about"> About </Link>
                    </li>

                </ul>

                <Route exact path='/' component={HomeComponent}></Route>
                <Route exact path='/about' component={AboutComponent}></Route>
                <Route exact path='/scan' component={QrScannerComponent}></Route>
                <Route exact path='/registerBag/:id' component={RegisterBagComponent}></Route>
                <Redirect to="/" />

            </div>
        </Router>
    );
  }
}

export default App;
