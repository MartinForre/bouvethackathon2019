import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './App.css';
import { FaHome, FaUserCircle, FaQrcode } from 'react-icons/fa';

import Routes from './containers/navigation';

class App extends Component {

	renderHeader = () => (
		<div id="nav-container">
      		<nav>
				<ul>
					<li>
						<Link to="/home"> <FaHome/> </Link>
					</li>
					<li>
						<Link to="/"> <FaQrcode/> </Link>
					</li>
					<li>
						<Link to="/profile"> <FaUserCircle/> </Link>
					</li>
				</ul>
      		</nav>
		</div>
	);

  render() {
		return (
			<div>
				<Routes />
				{this.renderHeader()}
			</div>
		);
  }
}

export default App;
