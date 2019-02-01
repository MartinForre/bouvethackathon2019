import React, { Component } from 'react';
import { HashRouter as Router, Route, Link } from 'react-router-dom'
import './App.css';
import HomeComponent from './components/HomeComponent';
import AboutComponent from './components/AboutComponent';
import TeamComponent from './components/TeamComponent';

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
                        <Link to="/about"> About </Link>
                    </li>
                    <li>
                        <Link to="/team"> Team </Link>
                    </li>
                </ul>

                <Route exact path='/' component={HomeComponent}></Route>
                <Route exact path='/about' component={AboutComponent}></Route>
                <Route exact path='/team' component={TeamComponent}></Route>

            </div>
        </Router>
    );
  }
}

export default App;
