import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './App.css';

import Routes from './containers/navigation';

class App extends Component {

	renderHeader = () => (
		<div id="nav-container">
      		<nav>
				<ul>
					<li>
						<Link to="/"> Home </Link>
					</li>
					<li>
						<Link to="/about"> About </Link>
					</li>
				</ul>
      		</nav>
		</div>
	);

  render() {
		return (
			<div>
				{this.renderHeader()}
				<Routes />
			</div>
		);
  }
}

export default App;
